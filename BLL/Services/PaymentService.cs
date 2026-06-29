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

        // ── Member Payments ──────────────────────────────────────────────────────
        public async Task<IEnumerable<PaymentDTO>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(MapToDTO);

        public async Task<IEnumerable<PaymentDTO>> GetFilteredAsync(string? search, int? month, int? year) =>
            (await _repo.GetFilteredAsync(search, month, year)).Select(MapToDTO);

        public async Task<IEnumerable<PaymentDTO>> GetByMemberIdAsync(int memberId) =>
            (await _repo.GetByMemberIdAsync(memberId)).Select(MapToDTO);

        public async Task<bool> RecordMemberPaymentAsync(int paymentId, decimal amountPaid, string paymentMethod, string? notes)
        {
            var p = await _repo.GetByIdAsync(paymentId);
            if (p == null) return false;
            p.AmountPaid = Math.Min(amountPaid, p.TotalAmount); // Can't pay more than total
            p.PaymentMethod = paymentMethod;
            p.PaymentDate = DateTime.Now;
            p.PaymentStatus = p.AmountPaid >= p.TotalAmount ? "Paid"
                            : p.AmountPaid > 0 ? "Partial Paid"
                            : "Unpaid";
            if (notes != null) p.Notes = notes;
            await _repo.UpdateAsync(p);
            return true;
        }

        public async Task<bool> MarkAsPaidAsync(int paymentId)
        {
            var p = await _repo.GetByIdAsync(paymentId);
            if (p == null) return false;
            p.AmountPaid = p.TotalAmount; p.PaymentStatus = "Paid"; p.PaymentDate = DateTime.Now;
            await _repo.UpdateAsync(p); return true;
        }

        public async Task<bool> MarkAsUnpaidAsync(int paymentId)
        {
            var p = await _repo.GetByIdAsync(paymentId);
            if (p == null) return false;
            p.AmountPaid = 0; p.PaymentStatus = "Unpaid"; p.PaymentDate = null;
            await _repo.UpdateAsync(p); return true;
        }

        public async Task<bool> CreatePaymentAsync(int memberId, decimal amount, DateTime dueDate, int? purchaseId, string? packageName = null, string? notes = null)
        {
            await _repo.AddAsync(new Payment
            {
                MemberId = memberId, TotalAmount = amount, DueDate = dueDate,
                MembershipPurchaseId = purchaseId, PackageName = packageName,
                PaymentStatus = "Unpaid", PaymentMethod = "Cash", Notes = notes
            });
            return true;
        }

        public async Task<decimal> GetMonthlyRevenueAsync(int month, int year) =>
            await _repo.GetMonthlyRevenueAsync(month, year);

        // ── Trainer Salary Payments ──────────────────────────────────────────────
        public async Task<IEnumerable<TrainerPaymentDTO>> GetAllTrainerPaymentsAsync() =>
            (await _repo.GetAllTrainerPaymentsAsync()).Select(MapTrainerPaymentToDTO);

        public async Task<IEnumerable<TrainerPaymentDTO>> GetFilteredTrainerPaymentsAsync(int? trainerId, int? month, int? year) =>
            (await _repo.GetFilteredTrainerPaymentsAsync(trainerId, month, year)).Select(MapTrainerPaymentToDTO);

        public async Task<IEnumerable<TrainerPaymentDTO>> GetTrainerSalaryHistoryAsync(int trainerId) =>
            (await _repo.GetTrainerPaymentsByTrainerIdAsync(trainerId)).Select(MapTrainerPaymentToDTO);

        public async Task<bool> PayTrainerSalaryAsync(int trainerPaymentId, decimal amountToPay, string paymentMethod, string? notes)
        {
            var tp = await _repo.GetTrainerPaymentByIdAsync(trainerPaymentId);
            if (tp == null) return false;
            tp.AmountPaid = Math.Min(tp.AmountPaid + amountToPay, tp.TotalSalary);
            tp.PaymentMethod = paymentMethod;
            tp.LastPaidDate = DateTime.Now;
            tp.PaymentStatus = tp.AmountPaid >= tp.TotalSalary ? "Paid"
                             : tp.AmountPaid > 0 ? "Partial Paid"
                             : "Unpaid";
            if (notes != null) tp.Notes = notes;
            await _repo.UpdateTrainerPaymentAsync(tp);
            return true;
        }

        public async Task<bool> CreateTrainerPaymentAsync(int trainerId, int month, int year, decimal totalSalary)
        {
            await _repo.AddTrainerPaymentAsync(new TrainerPayment
            { TrainerId = trainerId, Month = month, Year = year, TotalSalary = totalSalary, AmountPaid = 0, PaymentStatus = "Unpaid" });
            return true;
        }

        // ── Personal Training Sessions ────────────────────────────────────────────
        public async Task<IEnumerable<PersonalTrainingSessionDTO>> GetAllPTSessionsAsync() =>
            (await _repo.GetAllPTSessionsAsync()).Select(MapPTSessionToDTO);

        public async Task<IEnumerable<PersonalTrainingSessionDTO>> GetFilteredPTSessionsAsync(int? trainerId, int? month, int? year) =>
            (await _repo.GetFilteredPTSessionsAsync(trainerId, month, year)).Select(MapPTSessionToDTO);

        public async Task<bool> CreatePTSessionAsync(int trainerId, int memberId, DateTime date, string timeSlot, decimal charge)
        {
            // Check slot capacity
            var trainer = await _repo.GetPTSlotBookingsAsync(trainerId, date, timeSlot);
            await _repo.AddPTSessionAsync(new PersonalTrainingSession
            {
                TrainerId = trainerId, MemberId = memberId, SessionDate = date,
                TimeSlot = timeSlot, ChargePerSession = charge, AmountPaid = 0, PaymentStatus = "Unpaid"
            });
            return true;
        }

        public async Task<bool> PayPTSessionAsync(int sessionId, decimal amountPaid, string paymentMethod, string? notes)
        {
            var s = await _repo.GetPTSessionByIdAsync(sessionId);
            if (s == null) return false;
            s.AmountPaid = Math.Min(amountPaid, s.ChargePerSession);
            s.PaymentMethod = paymentMethod;
            s.PaidDate = DateTime.Now;
            s.PaymentStatus = s.AmountPaid >= s.ChargePerSession ? "Paid"
                            : s.AmountPaid > 0 ? "Partial Paid"
                            : "Unpaid";
            if (notes != null) s.Notes = notes;
            await _repo.UpdatePTSessionAsync(s);
            return true;
        }

        // ── Mapping ──────────────────────────────────────────────────────────────
        private static PaymentDTO MapToDTO(Payment p) => new()
        {
            Id = p.Id, MemberId = p.MemberId,
            MemberName = p.Member?.User != null ? $"{p.Member.User.FirstName} {p.Member.User.LastName}" : "",
            PackageName = p.PackageName, TotalAmount = p.TotalAmount, AmountPaid = p.AmountPaid,
            PaymentStatus = p.PaymentStatus, PaymentMethod = p.PaymentMethod,
            PaymentDate = p.PaymentDate, DueDate = p.DueDate, Notes = p.Notes, CreatedAt = p.CreatedAt
        };

        private static TrainerPaymentDTO MapTrainerPaymentToDTO(TrainerPayment tp) => new()
        {
            Id = tp.Id, TrainerId = tp.TrainerId,
            TrainerName = tp.Trainer?.User != null ? $"{tp.Trainer.User.FirstName} {tp.Trainer.User.LastName}" : "",
            Month = tp.Month, Year = tp.Year, TotalSalary = tp.TotalSalary, AmountPaid = tp.AmountPaid,
            PaymentStatus = tp.PaymentStatus, PaymentMethod = tp.PaymentMethod, LastPaidDate = tp.LastPaidDate, Notes = tp.Notes
        };

        private static PersonalTrainingSessionDTO MapPTSessionToDTO(PersonalTrainingSession s) => new()
        {
            Id = s.Id, TrainerId = s.TrainerId,
            TrainerName = s.Trainer?.User != null ? $"{s.Trainer.User.FirstName} {s.Trainer.User.LastName}" : "",
            MemberId = s.MemberId,
            MemberName = s.Member?.User != null ? $"{s.Member.User.FirstName} {s.Member.User.LastName}" : "",
            SessionDate = s.SessionDate, TimeSlot = s.TimeSlot, ChargePerSession = s.ChargePerSession,
            AmountPaid = s.AmountPaid, PaymentStatus = s.PaymentStatus, PaymentMethod = s.PaymentMethod,
            PaidDate = s.PaidDate, Notes = s.Notes, CreatedAt = s.CreatedAt
        };
    }
}

