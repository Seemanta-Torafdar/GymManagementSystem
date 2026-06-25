namespace DAL.EF.Models
{
    public class YogaSchedule
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty; // Mon, Wed, Fri
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Instructor { get; set; }
        public int Capacity { get; set; } = 20;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<MembershipPurchase> MembershipPurchases { get; set; } = new List<MembershipPurchase>();
    }
}
