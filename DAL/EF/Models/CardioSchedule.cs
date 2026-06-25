namespace DAL.EF.Models
{
    public class CardioSchedule
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? EquipmentUsed { get; set; }
        public string? Instructor { get; set; }
        public int Capacity { get; set; } = 25;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<MembershipPurchase> MembershipPurchases { get; set; } = new List<MembershipPurchase>();
    }
}
