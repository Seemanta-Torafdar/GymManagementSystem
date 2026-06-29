using DAL.EF.Models;

namespace DAL.Interfaces
{
    public interface IPaymentRepo
    {
        // Member Payments
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<Payment>> GetFilteredAsync(string? search, int? month, int? year);
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task<int> GetPendingCountAsync();
        Task<decimal> GetMonthlyRevenueAsync(int month, int year);

        // Trainer Salary Payments
        Task<IEnumerable<TrainerPayment>> GetAllTrainerPaymentsAsync();
        Task<IEnumerable<TrainerPayment>> GetFilteredTrainerPaymentsAsync(int? trainerId, int? month, int? year);
        Task<TrainerPayment?> GetTrainerPaymentByIdAsync(int id);
        Task<IEnumerable<TrainerPayment>> GetTrainerPaymentsByTrainerIdAsync(int trainerId);
        Task AddTrainerPaymentAsync(TrainerPayment payment);
        Task UpdateTrainerPaymentAsync(TrainerPayment payment);

        // Personal Training Sessions
        Task<IEnumerable<PersonalTrainingSession>> GetAllPTSessionsAsync();
        Task<IEnumerable<PersonalTrainingSession>> GetFilteredPTSessionsAsync(int? trainerId, int? month, int? year);
        Task<PersonalTrainingSession?> GetPTSessionByIdAsync(int id);
        Task<int> GetPTSlotBookingsAsync(int trainerId, DateTime date, string timeSlot);
        Task AddPTSessionAsync(PersonalTrainingSession session);
        Task UpdatePTSessionAsync(PersonalTrainingSession session);
    }
}

