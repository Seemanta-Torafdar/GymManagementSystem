namespace BLL.DTOs
{
    public class MembershipPackageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public string? Benefits { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public List<string> BenefitList => Benefits?.Split(';').ToList() ?? new();
    }

    public class MembershipPurchaseDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public int PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public int ShiftId { get; set; }
        public string ShiftName { get; set; } = string.Empty;
        public int? YogaScheduleId { get; set; }
        public string? YogaClassName { get; set; }
        public int? CardioScheduleId { get; set; }
        public string? CardioClassName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public int RemainingDays => Math.Max(0, (EndDate - DateTime.Today).Days);
    }

    public class GymShiftDTO
    {
        public int Id { get; set; }
        public string ShiftName { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public string TimeRange => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
    }

    public class YogaScheduleDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Instructor { get; set; }
        public int Capacity { get; set; }
    }

    public class CardioScheduleDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? EquipmentUsed { get; set; }
        public string? Instructor { get; set; }
        public int Capacity { get; set; }
    }
}
