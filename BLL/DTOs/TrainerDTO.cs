using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class TrainerDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int Experience { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal TrainingCharge { get; set; }
        public string? Bio { get; set; }
        public string? Certifications { get; set; }
        public string? ProfilePhoto { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime JoinDate { get; set; }
        public int AssignedMembersCount { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public List<TrainerAssignmentDTO> Assignments { get; set; } = new();
    }

    public class TrainerCreateDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required."), EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required."), MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specialization is required.")]
        public string Specialization { get; set; } = string.Empty;

        [Range(0, 60, ErrorMessage = "Experience must be between 0 and 60 years.")]
        public int Experience { get; set; }

        [Range(0, 9999999, ErrorMessage = "Please enter a valid salary.")]
        public decimal MonthlySalary { get; set; }

        [Range(0, 9999999, ErrorMessage = "Please enter a valid training charge.")]
        public decimal TrainingCharge { get; set; }

        public string? Bio { get; set; }
        public string? Certifications { get; set; }
        public string? ProfilePhoto { get; set; }
    }

    public class TrainerEditDTO
    {
        public int Id { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public int Experience { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal TrainingCharge { get; set; }
        public string? Bio { get; set; }
        public string? Certifications { get; set; }
        public bool IsAvailable { get; set; }
        public string? ProfilePhoto { get; set; }
    }

    public class TrainerAssignmentDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string? MemberPhoto { get; set; }
        public string? WorkoutPlan { get; set; }
        public string? TrainingNotes { get; set; }
        public DateTime AssignedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
