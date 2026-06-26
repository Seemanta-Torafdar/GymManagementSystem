using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _repo;
        public PaymentService(IPaymentRepo repo) { _repo = repo; }

        public async Task<IEnumerable<PaymentDTO>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(MapToDTO);

        public async Task<IEnumerable<PaymentDTO>> GetByMemberIdAsync(int memberId) =>
            (await _repo.GetByMemberIdAsync(memberId)).Select(MapToDTO);

        public async Task<bool> MarkAsPaidAsync(int paymentId)
        {
            var p = await _repo.GetByIdAsync(paymentId);
            if (p == null) return false;
            p.Status = "Paid"; p.PaymentDate = DateTime.Now;
            await _repo.UpdateAsync(p); return true;
        }

        public async Task<bool> MarkAsUnpaidAsync(int paymentId)
        {
            var p = await _repo.GetByIdAsync(paymentId);
            if (p == null) return false;
            p.Status = "Pending"; p.PaymentDate = null;
            await _repo.UpdateAsync(p); return true;
        }

        public async Task<bool> CreatePaymentAsync(int memberId, decimal amount, DateTime dueDate, int? purchaseId, string? notes = null)
        {
            await _repo.AddAsync(new Payment
            {
                MemberId = memberId, Amount = amount, DueDate = dueDate,
                MembershipPurchaseId = purchaseId, Status = "Pending", PaymentMethod = "Cash",
                Notes = notes
            });
            return true;
        }

        public async Task<decimal> GetMonthlyRevenueAsync(int month, int year) =>
            await _repo.GetMonthlyRevenueAsync(month, year);

        public async Task<IEnumerable<TrainerPaymentDTO>> GetAllTrainerPaymentsAsync() =>
            (await _repo.GetAllTrainerPaymentsAsync()).Select(tp => new TrainerPaymentDTO
            {
                Id = tp.Id, TrainerId = tp.TrainerId,
                TrainerName = tp.Trainer?.User != null ? $"{tp.Trainer.User.FirstName} {tp.Trainer.User.LastName}" : "",
                Month = tp.Month, Year = tp.Year, Amount = tp.Amount, Status = tp.Status, PaidDate = tp.PaidDate
            });

        public async Task<bool> MarkTrainerPaidAsync(int trainerPaymentId)
        {
            var tp = await _repo.GetTrainerPaymentByIdAsync(trainerPaymentId);
            if (tp == null) return false;
            tp.Status = "Paid"; tp.PaidDate = DateTime.Now;
            await _repo.UpdateTrainerPaymentAsync(tp); return true;
        }

        public async Task<bool> CreateTrainerPaymentAsync(int trainerId, int month, int year, decimal amount)
        {
            await _repo.AddTrainerPaymentAsync(new TrainerPayment
            { TrainerId = trainerId, Month = month, Year = year, Amount = amount, Status = "Pending" });
            return true;
        }

        private PaymentDTO MapToDTO(Payment p) => new()
        {
            Id = p.Id, MemberId = p.MemberId,
            MemberName = p.Member?.User != null ? $"{p.Member.User.FirstName} {p.Member.User.LastName}" : "",
            Amount = p.Amount, Status = p.Status, PaymentMethod = p.PaymentMethod,
            PaymentDate = p.PaymentDate, DueDate = p.DueDate, Notes = p.Notes, CreatedAt = p.CreatedAt
        };
    }
}
