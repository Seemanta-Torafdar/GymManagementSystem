using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly GymDbContext _context;
        public PaymentRepo(GymDbContext context) { _context = context; }

        public async Task<IEnumerable<Payment>> GetAllAsync() =>
            await _context.Payments.Include(p => p.Member).ThenInclude(m => m.User).Where(p => p.MembershipPurchaseId != null).OrderByDescending(p => p.CreatedAt).ToListAsync();

        public async Task<Payment?> GetByIdAsync(int id) =>
            await _context.Payments.Include(p => p.Member).ThenInclude(m => m.User).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Payment>> GetByMemberIdAsync(int memberId) =>
            await _context.Payments.Include(p => p.MembershipPurchase).Where(p => p.MemberId == memberId).OrderByDescending(p => p.CreatedAt).ToListAsync();

        public async Task<IEnumerable<Payment>> GetFilteredAsync(string? search, int? month, int? year, DateTime? date = null, string? packageName = null, string? paymentStatus = null)
        {
            var query = _context.Payments.Include(p => p.Member).ThenInclude(m => m.User).Where(p => p.MembershipPurchaseId != null).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                var q = search.Trim().ToLower();
                var parts = q.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                if (parts.Length == 2)
                {
                    var p1 = parts[0];
                    var p2 = parts[1];
                    query = query.Where(p => 
                        (p.Member.User.Email != null && p.Member.User.Email.ToLower() == q) || 
                        (p.Member.GymId != null && p.Member.GymId.ToLower() == q) ||
                        (p.Member.User.FirstName.ToLower().Contains(p1) && p.Member.User.LastName.ToLower().Contains(p2)) ||
                        (p.Member.User.FirstName.ToLower().Contains(p2) && p.Member.User.LastName.ToLower().Contains(p1)) ||
                        (p.Member.User.FirstName + " " + p.Member.User.LastName).ToLower().Contains(q) ||
                        (p.Notes != null && p.Notes.ToLower().Contains(q)));
                }
                else
                {
                    query = query.Where(p => 
                        p.Member.User.FirstName.ToLower().Contains(q) || 
                        p.Member.User.LastName.ToLower().Contains(q) || 
                        (p.Member.User.FirstName + " " + p.Member.User.LastName).ToLower().Contains(q) || 
                        (p.Member.User.Email != null && p.Member.User.Email.ToLower() == q) || 
                        (p.Member.GymId != null && p.Member.GymId.ToLower() == q) || 
                        (p.Notes != null && p.Notes.ToLower().Contains(q)));
                }
            }
            if (!string.IsNullOrEmpty(packageName))
                query = query.Where(p => p.PackageName == packageName);
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                if (paymentStatus == "Paid")
                    query = query.Where(p => p.PaymentStatus == "Paid");
                else if (paymentStatus == "Unpaid")
                    query = query.Where(p => p.PaymentStatus != "Paid");
            }
            if (month.HasValue)
                query = query.Where(p => p.PaymentDate.HasValue && p.PaymentDate.Value.Month == month);
            if (year.HasValue)
                query = query.Where(p => p.PaymentDate.HasValue && p.PaymentDate.Value.Year == year || (p.DueDate.Month == (month ?? p.DueDate.Month) && p.DueDate.Year == year));
            if (date.HasValue)
                query = query.Where(p => (p.PaymentDate.HasValue && p.PaymentDate.Value.Date == date.Value.Date) || p.DueDate.Date == date.Value.Date);
            return await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task AddAsync(Payment payment) { await _context.Payments.AddAsync(payment); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Payment payment) 
        {
            // Attach only the Payment entity and mark it as modified.
            // Using _context.Payments.Update() with a tracked navigation (Member) causes
            // EF to attempt to update related entities, leading to conflicts and stale data.
            var entry = _context.Entry(payment);
            if (entry.State == EntityState.Detached)
                _context.Payments.Attach(payment);
            entry.State = EntityState.Modified;
            // Do not touch the Member navigation — only update the Payment scalar fields.
            entry.Reference(p => p.Member).IsModified = false;

            // Also sync the MembershipPurchase.PaymentStatus if linked
            if (payment.MembershipPurchaseId.HasValue)
            {
                var purchase = await _context.MembershipPurchases.FindAsync(payment.MembershipPurchaseId.Value);
                if (purchase != null)
                    purchase.PaymentStatus = payment.PaymentStatus == "Paid" ? "Paid" : "Pending";
            }
            
            await _context.SaveChangesAsync(); 
        }

        public async Task<int> GetPendingCountAsync() =>
            await _context.Payments.CountAsync(p => p.PaymentStatus == "Unpaid" || p.PaymentStatus == "Partial Paid");

        public async Task<decimal> GetMonthlyRevenueAsync(int month, int year) =>
            await _context.Payments.Where(p => p.PaymentDate.HasValue && p.PaymentDate.Value.Month == month && p.PaymentDate.Value.Year == year).SumAsync(p => p.AmountPaid);

        // Trainer Payments
        public async Task<IEnumerable<TrainerPayment>> GetAllTrainerPaymentsAsync() =>
            await _context.TrainerPayments.Include(tp => tp.Trainer).ThenInclude(t => t.User).OrderByDescending(tp => tp.Year).ThenByDescending(tp => tp.Month).ToListAsync();

        public async Task<IEnumerable<TrainerPayment>> GetFilteredTrainerPaymentsAsync(string? search, int? trainerId, int? month, int? year, DateTime? date = null)
        {
            var query = _context.TrainerPayments.Include(p => p.Trainer).ThenInclude(t => t.User).AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Trainer.User.FirstName.Contains(search) || p.Trainer.User.LastName.Contains(search));
            if (trainerId.HasValue) query = query.Where(p => p.TrainerId == trainerId.Value);
            if (month.HasValue) query = query.Where(p => p.Month == month.Value);
            if (year.HasValue) query = query.Where(p => p.Year == year.Value);
            if (date.HasValue) query = query.Where(p => p.LastPaidDate.HasValue && p.LastPaidDate.Value.Date == date.Value.Date);
            return await query.OrderByDescending(p => p.Year).ThenByDescending(p => p.Month).ToListAsync();
        }

        public async Task<TrainerPayment?> GetTrainerPaymentByIdAsync(int id) =>
            await _context.TrainerPayments.Include(tp => tp.Trainer).ThenInclude(t => t.User).FirstOrDefaultAsync(tp => tp.Id == id);

        public async Task<IEnumerable<TrainerPayment>> GetTrainerPaymentsByTrainerIdAsync(int trainerId) =>
            await _context.TrainerPayments.Where(tp => tp.TrainerId == trainerId).OrderByDescending(tp => tp.Year).ThenByDescending(tp => tp.Month).ToListAsync();

        public async Task AddTrainerPaymentAsync(TrainerPayment payment) { await _context.TrainerPayments.AddAsync(payment); await _context.SaveChangesAsync(); }
        public async Task UpdateTrainerPaymentAsync(TrainerPayment payment) { _context.TrainerPayments.Update(payment); await _context.SaveChangesAsync(); }

        // Personal Training Sessions
        public async Task<IEnumerable<PersonalTrainingSession>> GetAllPTSessionsAsync() =>
            await _context.PersonalTrainingSessions.Include(s => s.Trainer).ThenInclude(t => t.User).Include(s => s.Member).ThenInclude(m => m.User).OrderByDescending(s => s.SessionDate).ToListAsync();

        public async Task<IEnumerable<PersonalTrainingSession>> GetFilteredPTSessionsAsync(int? trainerId, int? month, int? year, DateTime? date = null, string? status = null, string? paymentMethod = null)
        {
            var query = _context.PersonalTrainingSessions
                .Include(s => s.Trainer).ThenInclude(t => t.User)
                .Include(s => s.Member).ThenInclude(m => m.User)
                .AsQueryable();
            if (trainerId.HasValue) query = query.Where(s => s.TrainerId == trainerId.Value);
            if (month.HasValue) query = query.Where(s => s.SessionDate.Month == month.Value);
            if (year.HasValue) query = query.Where(s => s.SessionDate.Year == year.Value);
            if (date.HasValue) query = query.Where(s => s.SessionDate.Date == date.Value.Date || (s.PaidDate.HasValue && s.PaidDate.Value.Date == date.Value.Date));
            if (!string.IsNullOrEmpty(status)) query = query.Where(s => s.PaymentStatus == status);
            if (!string.IsNullOrEmpty(paymentMethod)) query = query.Where(s => s.PaymentMethod == paymentMethod);
            return await query.OrderByDescending(s => s.SessionDate).ToListAsync();
        }

        public async Task<PersonalTrainingSession?> GetPTSessionByIdAsync(int id) =>
            await _context.PersonalTrainingSessions.Include(s => s.Trainer).ThenInclude(t => t.User).Include(s => s.Member).ThenInclude(m => m.User).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<int> GetPTSlotBookingsAsync(int trainerId, DateTime date, string timeSlot) =>
            await _context.PersonalTrainingSessions.CountAsync(s => s.TrainerId == trainerId && s.SessionDate.Date == date.Date && s.TimeSlot == timeSlot);

        public async Task AddPTSessionAsync(PersonalTrainingSession session) { await _context.PersonalTrainingSessions.AddAsync(session); await _context.SaveChangesAsync(); }
        public async Task UpdatePTSessionAsync(PersonalTrainingSession session) { _context.PersonalTrainingSessions.Update(session); await _context.SaveChangesAsync(); }

        // Personal Training Fee Payments
        public async Task<IEnumerable<Payment>> GetPTFeePaymentsByMemberIdAsync(int memberId) =>
            await _context.Payments
                .Include(p => p.Member).ThenInclude(m => m.User)
                .Where(p => p.MemberId == memberId && p.PackageName != null && p.PackageName.StartsWith("PT Fee"))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

        public async Task<IEnumerable<Payment>> GetPTFeePaymentsForTrainerStudentsAsync(int trainerId, int? month, int? year, string? paymentStatus = null)
        {
            // Get all active member IDs assigned to this trainer
            var memberIds = await _context.TrainerAssignments
                .Where(ta => ta.TrainerId == trainerId && ta.IsActive)
                .Select(ta => ta.MemberId)
                .ToListAsync();

            var query = _context.Payments
                .Include(p => p.Member).ThenInclude(m => m.User)
                .Where(p => memberIds.Contains(p.MemberId) && p.PackageName != null && p.PackageName.StartsWith("PT Fee"));

            if (month.HasValue)
                query = query.Where(p => p.DueDate.Month == month.Value);
            if (year.HasValue)
                query = query.Where(p => p.DueDate.Year == year.Value);
                
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                if (paymentStatus == "Paid")
                    query = query.Where(p => p.PaymentStatus == "Paid");
                else if (paymentStatus == "Unpaid")
                    query = query.Where(p => p.PaymentStatus != "Paid");
            }

            return await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }
    }
}

