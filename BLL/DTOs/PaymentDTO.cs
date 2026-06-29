namespace BLL.DTOs
{
    // ── Member Payment ───────────────────────────────────────────────────────────
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string? PackageName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal DueAmount => TotalAmount - AmountPaid;
        public string PaymentStatus { get; set; } = "Unpaid";  // Unpaid | Partial Paid | Paid
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // ── Trainer Salary Payment ───────────────────────────────────────────────────
    public class TrainerPaymentDTO
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal DueAmount => TotalSalary - AmountPaid;
        public string PaymentStatus { get; set; } = "Unpaid";  // Unpaid | Partial Paid | Paid
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime? LastPaidDate { get; set; }
        public string? Notes { get; set; }
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
    }

    // ── Personal Training Session ────────────────────────────────────────────────
    public class PersonalTrainingSessionDTO
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public DateTime SessionDate { get; set; }
        public string TimeSlot { get; set; } = string.Empty;
        public decimal ChargePerSession { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal DueAmount => ChargePerSession - AmountPaid;
        public string PaymentStatus { get; set; } = "Unpaid";  // Unpaid | Partial Paid | Paid
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime? PaidDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // ── Admin Dashboard ──────────────────────────────────────────────────────────
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

        // Revenue breakdown
        public decimal TotalSalaryPaid { get; set; }
        public decimal TotalSalaryDue { get; set; }
        public int PaidMembersCount { get; set; }
        public int PartialPaidMembersCount { get; set; }
        public int UnpaidMembersCount { get; set; }

        public List<PaymentDTO> RecentPayments { get; set; } = new();
        public List<MemberDTO> RecentMembers { get; set; } = new();
        public List<TrainerPaymentDTO> PendingTrainerPaymentList { get; set; } = new();
    }
}

