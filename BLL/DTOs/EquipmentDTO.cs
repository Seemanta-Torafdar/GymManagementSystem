namespace BLL.DTOs
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; }

        // Admin-only inventory info
        public int? Quantity { get; set; }
        public string? StockStatus { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string? Supplier { get; set; }
    }

    public class NotificationDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ReviewDTO
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }

    public class GymSettingDTO
    {
        public int Id { get; set; }
        public string GymName { get; set; } = string.Empty;
        public string? LogoPath { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? AboutUs { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? YouTubeUrl { get; set; }
        public string? BannerImage1 { get; set; }
        public string? BannerImage2 { get; set; }
        public string? BannerImage3 { get; set; }
        public string? HeroTagline { get; set; }
    }
}
