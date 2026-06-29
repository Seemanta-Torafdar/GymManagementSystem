namespace DAL.EF.Models
{
    public class TrainerPayment
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        // Salary fields
        public decimal TotalSalary { get; set; }       // The full monthly salary amount
        public decimal AmountPaid { get; set; } = 0;   // How much has been paid so far
        public decimal DueAmount => TotalSalary - AmountPaid; // Auto-calculated

        // Status: Unpaid | Partial Paid | Paid
        public string PaymentStatus { get; set; } = "Unpaid";

        // Last payment details
        public string PaymentMethod { get; set; } = "Cash"; // Cash, Bank Transfer, bKash
        public DateTime? LastPaidDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Trainer Trainer { get; set; } = null!;
    }
}

