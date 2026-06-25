namespace DAL.EF.Models
{
    public class TrainerReview
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int MemberId { get; set; }
        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; } = true;

        public virtual Trainer Trainer { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
