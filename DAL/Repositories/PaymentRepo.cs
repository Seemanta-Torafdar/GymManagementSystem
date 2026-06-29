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
            await _context.Payments.Include(p => p.Member).ThenInclude(m => m.User).OrderByDescending(p => p.CreatedAt).ToListAsync();

        public async Task<Payment?> GetByIdAsync(int id) =>
            await _context.Payments.Include(p => p.Member).ThenInclude(m => m.User).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Payment>> GetByMemberIdAsync(int memberId) =>
            await _context.Payments.Where(p => p.MemberId == memberId).OrderByDescending(p => p.CreatedAt).ToListAsync();

        public async Task<IEnumerable<Payment>> GetFilteredAsync(string? search, int? month, int? year)
        {
            var query = _context.Payments.Include(p => p.Member).ThenInclude(m => m.User).AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Member.User.FirstName.Contains(search) || p.Member.User.LastName.Contains(search) || (p.Notes != null && p.Notes.Contains(search)));
            if (month.HasValue)
                query = query.Where(p => p.PaymentDate.HasValue && p.PaymentDate.Value.Month == month);
            if (year.HasValue)
                query = query.Where(p => p.PaymentDate.HasValue && p.PaymentDate.Value.Year == year || (p.DueDate.Month == (month ?? p.DueDate.Month) && p.DueDate.Year == year));
            return await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task AddAsync(Payment payment) { await _context.Payments.AddAsync(payment); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Payment payment) { _context.Payments.Update(payment); await _context.SaveChangesAsync(); }

        public async Task<int> GetPendingCountAsync() =>
            await _context.Payments.CountAsync(p => p.PaymentStatus == "Unpaid" || p.PaymentStatus == "Partial Paid");

        public async Task<decimal> GetMonthlyRevenueAsync(int month, int year) =>
            await _context.Payments.Where(p => p.PaymentDate.HasValue && p.PaymentDate.Value.Month == month && p.PaymentDate.Value.Year == year).SumAsync(p => p.AmountPaid);

        // Trainer Payments
        public async Task<IEnumerable<TrainerPayment>> GetAllTrainerPaymentsAsync() =>
            await _context.TrainerPayments.Include(tp => tp.Trainer).ThenInclude(t => t.User).OrderByDescending(tp => tp.Year).ThenByDescending(tp => tp.Month).ToListAsync();

        public async Task<IEnumerable<TrainerPayment>> GetFilteredTrainerPaymentsAsync(int? trainerId, int? month, int? year)
        {
            var query = _context.TrainerPayments.Include(tp => tp.Trainer).ThenInclude(t => t.User).AsQueryable();
            if (trainerId.HasValue) query = query.Where(tp => tp.TrainerId == trainerId);
            if (month.HasValue) query = query.Where(tp => tp.Month == month);
            if (year.HasValue) query = query.Where(tp => tp.Year == year);
            return await query.OrderByDescending(tp => tp.Year).ThenByDescending(tp => tp.Month).ToListAsync();
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

        public async Task<IEnumerable<PersonalTrainingSession>> GetFilteredPTSessionsAsync(int? trainerId, int? month, int? year)
        {
            var query = _context.PersonalTrainingSessions.Include(s => s.Trainer).ThenInclude(t => t.User).Include(s => s.Member).ThenInclude(m => m.User).AsQueryable();
            if (trainerId.HasValue) query = query.Where(s => s.TrainerId == trainerId);
            if (month.HasValue) query = query.Where(s => s.SessionDate.Month == month);
            if (year.HasValue) query = query.Where(s => s.SessionDate.Year == year);
            return await query.OrderByDescending(s => s.SessionDate).ToListAsync();
        }

        public async Task<PersonalTrainingSession?> GetPTSessionByIdAsync(int id) =>
            await _context.PersonalTrainingSessions.Include(s => s.Trainer).ThenInclude(t => t.User).Include(s => s.Member).ThenInclude(m => m.User).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<int> GetPTSlotBookingsAsync(int trainerId, DateTime date, string timeSlot) =>
            await _context.PersonalTrainingSessions.CountAsync(s => s.TrainerId == trainerId && s.SessionDate.Date == date.Date && s.TimeSlot == timeSlot);

        public async Task AddPTSessionAsync(PersonalTrainingSession session) { await _context.PersonalTrainingSessions.AddAsync(session); await _context.SaveChangesAsync(); }
        public async Task UpdatePTSessionAsync(PersonalTrainingSession session) { _context.PersonalTrainingSessions.Update(session); await _context.SaveChangesAsync(); }
    }
}

