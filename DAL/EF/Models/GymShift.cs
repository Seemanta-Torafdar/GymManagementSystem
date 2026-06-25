namespace DAL.EF.Models
{
    public class GymShift
    {
        public int Id { get; set; }
        public string ShiftName { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<MembershipPurchase> MembershipPurchases { get; set; } = new List<MembershipPurchase>();
    }
}
