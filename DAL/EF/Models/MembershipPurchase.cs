namespace DAL.EF.Models
{
    public class MembershipPurchase
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int PackageId { get; set; }
        public int ShiftId { get; set; }
        public int? YogaScheduleId { get; set; }
        public int? CardioScheduleId { get; set; }
        public int? TrainerAssignmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public string? Notes { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual MembershipPackage Package { get; set; } = null!;
        public virtual GymShift Shift { get; set; } = null!;
        public virtual YogaSchedule? YogaSchedule { get; set; }
        public virtual CardioSchedule? CardioSchedule { get; set; }
    }
}
