namespace DAL.EF.Models
{
    public class MembershipPackage
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public string? Benefits { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<MembershipPurchase> MembershipPurchases { get; set; } = new List<MembershipPurchase>();
    }
}
