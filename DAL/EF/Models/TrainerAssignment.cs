namespace DAL.EF.Models
{
    public class TrainerAssignment
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int MemberId { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        public string? WorkoutPlan { get; set; }
        public string? TrainingNotes { get; set; }
        public decimal PersonalTrainingCharge { get; set; } = 0; // Monthly PT fee for this student (can vary per student)
        public bool IsActive { get; set; } = true;

        public virtual Trainer Trainer { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
