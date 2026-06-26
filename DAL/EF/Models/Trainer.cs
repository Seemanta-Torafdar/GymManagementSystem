namespace DAL.EF.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int Experience { get; set; } // years
        public decimal MonthlySalary { get; set; }
        public decimal TrainingCharge { get; set; } // Fee charged to member for personal training
        public string? Bio { get; set; }
        public string? Certifications { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime JoinDate { get; set; } = DateTime.Now;

        // Navigation
        public virtual User User { get; set; } = null!;
        public virtual ICollection<TrainerAssignment> TrainerAssignments { get; set; } = new List<TrainerAssignment>();
        public virtual ICollection<TrainerReview> TrainerReviews { get; set; } = new List<TrainerReview>();
        public virtual ICollection<TrainerPayment> TrainerPayments { get; set; } = new List<TrainerPayment>();
    }
}
