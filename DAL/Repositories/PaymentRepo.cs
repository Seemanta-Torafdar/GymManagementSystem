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
        public async Task<Payment?> GetByIdAsync(int id) => await _context.Payments.Include(p => p.Member).ThenInclude(m => m.User).FirstOrDefaultAsync(p => p.Id == id);
        public async Task<IEnumerable<Payment>> GetByMemberIdAsync(int memberId) =>
            await _context.Payments.Where(p => p.MemberId == memberId).OrderByDescending(p => p.CreatedAt).ToListAsync();
        public async Task AddAsync(Payment payment) { await _context.Payments.AddAsync(payment); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Payment payment) { _context.Payments.Update(payment); await _context.SaveChangesAsync(); }
        public async Task<int> GetPendingCountAsync() => await _context.Payments.CountAsync(p => p.Status == "Pending");
        public async Task<decimal> GetMonthlyRevenueAsync(int month, int year) =>
            await _context.Payments.Where(p => p.Status == "Paid" && p.PaymentDate.HasValue && p.PaymentDate.Value.Month == month && p.PaymentDate.Value.Year == year).SumAsync(p => p.Amount);

        // Trainer Payments
        public async Task<IEnumerable<TrainerPayment>> GetAllTrainerPaymentsAsync() =>
            await _context.TrainerPayments.Include(tp => tp.Trainer).ThenInclude(t => t.User).OrderByDescending(tp => tp.Year).ThenByDescending(tp => tp.Month).ToListAsync();
        public async Task<TrainerPayment?> GetTrainerPaymentByIdAsync(int id) => await _context.TrainerPayments.Include(tp => tp.Trainer).ThenInclude(t => t.User).FirstOrDefaultAsync(tp => tp.Id == id);
        public async Task<IEnumerable<TrainerPayment>> GetTrainerPaymentsByTrainerIdAsync(int trainerId) =>
            await _context.TrainerPayments.Where(tp => tp.TrainerId == trainerId).OrderByDescending(tp => tp.Year).ThenByDescending(tp => tp.Month).ToListAsync();
        public async Task AddTrainerPaymentAsync(TrainerPayment payment) { await _context.TrainerPayments.AddAsync(payment); await _context.SaveChangesAsync(); }
        public async Task UpdateTrainerPaymentAsync(TrainerPayment payment) { _context.TrainerPayments.Update(payment); await _context.SaveChangesAsync(); }
    }
}
