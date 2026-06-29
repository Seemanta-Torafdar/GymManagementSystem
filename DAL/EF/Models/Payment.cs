namespace DAL.EF.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int? MembershipPurchaseId { get; set; }
        public string? PackageName { get; set; }         // e.g. "Gold Package"
        public decimal TotalAmount { get; set; }         // Total fee owed
        public decimal AmountPaid { get; set; } = 0;    // How much paid so far
        public decimal DueAmount => TotalAmount - AmountPaid; // Auto-calculated

        // Status: Unpaid | Partial Paid | Paid
        public string PaymentStatus { get; set; } = "Unpaid";
        public string PaymentMethod { get; set; } = "Cash";
        public DateTime? PaymentDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Member Member { get; set; } = null!;
    }
}

