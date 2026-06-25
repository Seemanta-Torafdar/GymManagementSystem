namespace DAL.EF.Models
{
    public class GymSetting
    {
        public int Id { get; set; }
        public string GymName { get; set; } = "PowerFit Gym";
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
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
