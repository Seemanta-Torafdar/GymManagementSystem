namespace DAL.EF.Models
{
    public class PersonalTrainingSession
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int MemberId { get; set; }
        public DateTime SessionDate { get; set; }
        public string TimeSlot { get; set; } = string.Empty;
        public decimal ChargePerSession { get; set; }
        public decimal AmountPaid { get; set; } = 0;
        public decimal DueAmount => ChargePerSession - AmountPaid;
        public string PaymentStatus { get; set; } = "Unpaid";
        public string PaymentMethod { get; set; } = "Cash";
        public DateTime? PaidDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Trainer Trainer { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
