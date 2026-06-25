using Microsoft.AspNetCore.Identity;

namespace DAL.EF.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? ProfilePhoto { get; set; }
        public string Role { get; set; } = "Member"; // Admin, Trainer, Member
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        // Navigation
        public virtual Member? Member { get; set; }
        public virtual Trainer? Trainer { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
