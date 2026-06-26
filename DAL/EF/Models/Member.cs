namespace DAL.EF.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string GymId { get; set; } = string.Empty; // Sxxxxx
        public string UserId { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? Address { get; set; }
        public string? BloodGroup { get; set; }
        public string? MedicalNotes { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;

        // Navigation
        public virtual User User { get; set; } = null!;
        public virtual ICollection<MembershipPurchase> MembershipPurchases { get; set; } = new List<MembershipPurchase>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<TrainerAssignment> TrainerAssignments { get; set; } = new List<TrainerAssignment>();
        public virtual ICollection<TrainerReview> TrainerReviews { get; set; } = new List<TrainerReview>();
    }
}
