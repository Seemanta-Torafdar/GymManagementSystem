namespace BLL.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TrainerPaymentDTO
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal DueAmount => Amount - AmountPaid;
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime? PaidDate { get; set; }
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
    }

    public class AdminDashboardDTO
    {
        public int TotalMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int ActiveMemberships { get; set; }
        public int ExpiredMemberships { get; set; }
        public int PendingPayments { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public int PendingTrainerPayments { get; set; }
        public int LowStockEquipment { get; set; }
        public List<PaymentDTO> RecentPayments { get; set; } = new();
        public List<MemberDTO> RecentMembers { get; set; } = new();
        public List<TrainerPaymentDTO> PendingTrainerPaymentList { get; set; } = new();
    }
}
