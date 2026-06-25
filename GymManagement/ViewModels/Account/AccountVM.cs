using System.ComponentModel.DataAnnotations;

namespace GymManagement.ViewModels.Account
{
    public class LoginVM
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterVM
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required, DataType(DataType.Date), Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-20);

        public string? Address { get; set; }

        [Display(Name = "Blood Group")]
        public string? BloodGroup { get; set; }

        [Display(Name = "Emergency Contact Name")]
        public string? EmergencyContact { get; set; }

        [Display(Name = "Emergency Contact Phone")]
        public string? EmergencyPhone { get; set; }

        // Membership
        [Required, Range(1, int.MaxValue, ErrorMessage = "Please select a Membership Package.")]
        [Display(Name = "Membership Package")]
        public int PackageId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please select a Gym Shift.")]
        [Display(Name = "Gym Shift")]
        public int ShiftId { get; set; }

        [Display(Name = "Yoga Class (Optional)")]
        public int? YogaScheduleId { get; set; }

        [Display(Name = "Cardio Class (Optional)")]
        public int? CardioScheduleId { get; set; }
    }

    public class ChangePasswordVM
    {
        [Required, DataType(DataType.Password), Display(Name = "Current Password")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), MinLength(6), Display(Name = "New Password")]
        public string NewPassword { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
