namespace DAL.EF.Models
{
    public class TrainerPayment
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Paid
        public DateTime? PaidDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Trainer Trainer { get; set; } = null!;
    }
}
