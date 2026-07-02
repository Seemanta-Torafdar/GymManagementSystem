using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDTO>> GetAllAsync();
        Task<MemberDTO?> GetByIdAsync(int id);
        Task<MemberDTO?> GetByUserIdAsync(string userId);
        Task<(bool Success, IEnumerable<string> Errors)> CreateAsync(MemberCreateDTO dto);
        Task<bool> UpdateAsync(MemberEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MemberDTO>> SearchAsync(string query);
        Task<bool> UpdateProfilePhotoAsync(string userId, string photoPath);
    }

    public interface ITrainerService
    {
        Task<IEnumerable<TrainerDTO>> GetAllAsync();
        Task<TrainerDTO?> GetByIdAsync(int id);
        Task<TrainerDTO?> GetByUserIdAsync(string userId);
        Task<bool> CreateAsync(TrainerCreateDTO dto);
        Task<bool> UpdateAsync(TrainerEditDTO dto);
        Task<bool> UpdateProfileAsync(TrainerProfileEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateProfilePhotoAsync(string userId, string photoPath);
        Task<bool> AssignMemberAsync(int trainerId, int memberId, string? workoutPlan, string? notes, decimal personalTrainingCharge);
        Task<bool> RemoveTrainerAssignmentAsync(int memberId);
        Task<bool> UpdateWorkoutPlanAsync(int assignmentId, string workoutPlan, string notes);
    }

    public interface IMembershipService
    {
        Task<IEnumerable<MembershipPackageDTO>> GetAllPackagesAsync();
        Task<MembershipPackageDTO?> GetPackageByIdAsync(int id);
        Task<bool> CreatePackageAsync(MembershipPackageDTO dto);
        Task<bool> UpdatePackageAsync(MembershipPackageDTO dto);
        Task<bool> DeletePackageAsync(int id);

        Task<IEnumerable<MembershipPurchaseDTO>> GetAllPurchasesAsync();
        Task<MembershipPurchaseDTO?> GetActivePurchaseByMemberIdAsync(int memberId);
        Task<bool> PurchaseMembershipAsync(int memberId, int packageId, int shiftId, int? yogaId, int? cardioId);
        Task<bool> RenewMembershipAsync(int memberId, int packageId, int shiftId);
        Task<bool> UpdatePaymentStatusAsync(int purchaseId, string status);

        Task<IEnumerable<GymShiftDTO>> GetAllShiftsAsync();
        Task<bool> CreateShiftAsync(GymShiftDTO dto);
        Task<bool> UpdateShiftAsync(GymShiftDTO dto);
        Task<bool> DeleteShiftAsync(int id);

        Task<IEnumerable<YogaScheduleDTO>> GetAllYogaAsync();
        Task<IEnumerable<CardioScheduleDTO>> GetAllCardioAsync();
        Task<bool> CreateYogaAsync(YogaScheduleDTO dto);
        Task<bool> UpdateYogaAsync(YogaScheduleDTO dto);
        Task<bool> DeleteYogaAsync(int id);
        Task<bool> CreateCardioAsync(CardioScheduleDTO dto);
        Task<bool> UpdateCardioAsync(CardioScheduleDTO dto);
        Task<bool> DeleteCardioAsync(int id);
    }

    public interface IPaymentService
    {
        // Member Payments
        Task<IEnumerable<PaymentDTO>> GetAllAsync();
        Task<IEnumerable<PaymentDTO>> GetFilteredAsync(string? search, int? month, int? year, DateTime? date = null, string? packageName = null, string? paymentStatus = null);
        Task<IEnumerable<PaymentDTO>> GetByMemberIdAsync(int memberId);
        Task<bool> RecordMemberPaymentAsync(int paymentId, decimal amountPaid, string paymentMethod, string? notes);
        Task<bool> MarkAsPaidAsync(int paymentId);
        Task<bool> MarkAsUnpaidAsync(int paymentId);
        Task<bool> CreatePaymentAsync(int memberId, decimal amount, DateTime dueDate, int? purchaseId, string? packageName = null, string? notes = null);
        Task<PaymentDTO?> GetPaymentByIdAsync(int paymentId);
        Task<decimal> GetMonthlyRevenueAsync(int month, int year);

        // Trainer Salary
        Task<IEnumerable<TrainerPaymentDTO>> GetAllTrainerPaymentsAsync();
        Task<IEnumerable<TrainerPaymentDTO>> GetFilteredTrainerPaymentsAsync(string? search, int? trainerId, int? month, int? year, DateTime? date = null);
        Task EnsureTrainerPaymentsAsync(int month, int year);
        Task<IEnumerable<TrainerPaymentDTO>> GetTrainerSalaryHistoryAsync(int trainerId);
        Task<bool> PayTrainerSalaryAsync(int trainerPaymentId, decimal amountToPay, string paymentMethod, string? notes);
        Task<bool> CreateTrainerPaymentAsync(int trainerId, int month, int year, decimal totalSalary);

        // Personal Training Sessions
        Task<IEnumerable<PersonalTrainingSessionDTO>> GetAllPTSessionsAsync();
        Task<IEnumerable<PersonalTrainingSessionDTO>> GetFilteredPTSessionsAsync(int? trainerId, int? month, int? year, DateTime? date = null, string? status = null, string? paymentMethod = null);
        Task<bool> CreatePTSessionAsync(int trainerId, int memberId, DateTime sessionDate, string timeSlot, decimal charge);
        Task<bool> PayPTSessionAsync(int sessionId, decimal amountPaid, string paymentMethod, string? notes);

        // Personal Training Fee Status for Trainer Dashboard
        Task<IEnumerable<PaymentDTO>> GetStudentPaymentStatusAsync(int trainerId, int? month, int? year, string? paymentStatus = null);
    }

    public interface IEquipmentService
    {
        Task<IEnumerable<EquipmentDTO>> GetAllAsync();
        Task<EquipmentDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(EquipmentDTO dto, string? imagePath);
        Task<bool> UpdateAsync(EquipmentDTO dto, string? imagePath);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateInventoryAsync(int equipmentId, int quantity, string stockStatus, DateTime? purchaseDate, decimal? purchasePrice, string? supplier);
    }

    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetByUserIdAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task SendAsync(string userId, string title, string message, string type = "Info");
        Task MarkAsReadAsync(int id);
        Task MarkAllAsReadAsync(string userId);
        Task DeleteAsync(int id);
        Task CheckAndSendExpiryNotificationsAsync();
    }

    public interface IAdminService
    {
        Task<AdminDashboardDTO> GetDashboardDataAsync();
        Task<GymSettingDTO?> GetGymSettingsAsync();
        Task<bool> UpdateGymSettingsAsync(GymSettingDTO dto);
        Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync();
        Task<IEnumerable<ReviewDTO>> GetReviewsByTrainerIdAsync(int trainerId);
        Task<bool> AddReviewAsync(int memberId, int trainerId, int rating, string? comment);
        Task<bool> DeleteReviewAsync(int id);
        Task<bool> HasMemberReviewedTrainerAsync(int memberId, int trainerId);
    }
}
