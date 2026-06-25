namespace DAL.EF.Models
{
    public class EquipmentInventory
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public string StockStatus { get; set; } = "Available"; // Available, Low, OutOfStock
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string? Supplier { get; set; }
        public string? Notes { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public virtual Equipment Equipment { get; set; } = null!;
    }
}
