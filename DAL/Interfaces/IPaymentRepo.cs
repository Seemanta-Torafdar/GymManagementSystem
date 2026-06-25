using DAL.EF.Models;

namespace DAL.Interfaces
{
    public interface IPaymentRepo
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetByMemberIdAsync(int memberId);
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task<int> GetPendingCountAsync();
        Task<decimal> GetMonthlyRevenueAsync(int month, int year);

        Task<IEnumerable<TrainerPayment>> GetAllTrainerPaymentsAsync();
        Task<TrainerPayment?> GetTrainerPaymentByIdAsync(int id);
        Task<IEnumerable<TrainerPayment>> GetTrainerPaymentsByTrainerIdAsync(int trainerId);
        Task AddTrainerPaymentAsync(TrainerPayment payment);
        Task UpdateTrainerPaymentAsync(TrainerPayment payment);
    }
}
