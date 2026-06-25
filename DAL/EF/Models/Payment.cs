namespace DAL.EF.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int? MembershipPurchaseId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Paid
        public string PaymentMethod { get; set; } = "Cash";
        public DateTime? PaymentDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Member Member { get; set; } = null!;
    }
}
