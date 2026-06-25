namespace BLL.DTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? BloodGroup { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? MedicalNotes { get; set; }
        public string? ProfilePhoto { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }

        // Membership info
        public string? ActivePackageName { get; set; }
        public DateTime? MembershipStartDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }
        public int? RemainingDays { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ShiftName { get; set; }
        public string? YogaClassName { get; set; }
        public string? YogaTimeRange { get; set; }
        public string? CardioClassName { get; set; }
        public string? CardioTimeRange { get; set; }
        public string? AssignedTrainerName { get; set; }
    }

    public class MemberCreateDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? BloodGroup { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }

        // Membership selection
        public int PackageId { get; set; }
        public int ShiftId { get; set; }
        public int? YogaScheduleId { get; set; }
        public int? CardioScheduleId { get; set; }
        public int? TrainerId { get; set; }
    }

    public class MemberEditDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? BloodGroup { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? MedicalNotes { get; set; }
    }
}
