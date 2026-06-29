namespace DAL.EF.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string GymId { get; set; } = string.Empty; // Txxxxx
        public string UserId { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int Experience { get; set; } // years
        public decimal MonthlySalary { get; set; }
        public decimal TrainingCharge { get; set; } // General training fee
        public decimal PersonalTrainingCharge { get; set; } = 0; // Per 2-hour PT session charge
        public int MaxStudentsPerSlot { get; set; } = 2; // Max members per PT slot
        public string? AvailableTimeSlots { get; set; } // e.g. "08:00-10:00,10:00-12:00"
        public string? Bio { get; set; }
        public string? Certifications { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? TrainingTime { get; set; }

        // Navigation
        public virtual User User { get; set; } = null!;
        public virtual ICollection<TrainerAssignment> TrainerAssignments { get; set; } = new List<TrainerAssignment>();
        public virtual ICollection<TrainerReview> TrainerReviews { get; set; } = new List<TrainerReview>();
        public virtual ICollection<TrainerPayment> TrainerPayments { get; set; } = new List<TrainerPayment>();
        public virtual ICollection<PersonalTrainingSession> PersonalTrainingSessions { get; set; } = new List<PersonalTrainingSession>();
    }
}

