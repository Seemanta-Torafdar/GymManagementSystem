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
        public bool IsActive { get; set; } = true;

        public virtual Trainer Trainer { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
