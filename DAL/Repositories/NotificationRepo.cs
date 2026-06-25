using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly GymDbContext _context;
        public NotificationRepo(GymDbContext context) { _context = context; }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(string userId) =>
            await _context.Notifications.Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt).ToListAsync();
        public async Task<int> GetUnreadCountAsync(string userId) =>
            await _context.Notifications.CountAsync(n => n.UserId == userId && !n.IsRead);
        public async Task AddAsync(Notification notification) { await _context.Notifications.AddAsync(notification); await _context.SaveChangesAsync(); }
        public async Task MarkAsReadAsync(int id)
        {
            var n = await _context.Notifications.FindAsync(id);
            if (n != null) { n.IsRead = true; await _context.SaveChangesAsync(); }
        }
        public async Task MarkAllAsReadAsync(string userId)
        {
            var notifications = await _context.Notifications.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();
            notifications.ForEach(n => n.IsRead = true);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var n = await _context.Notifications.FindAsync(id);
            if (n != null) { _context.Notifications.Remove(n); await _context.SaveChangesAsync(); }
        }
    }
}
