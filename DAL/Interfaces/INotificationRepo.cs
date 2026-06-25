using DAL.EF.Models;

namespace DAL.Interfaces
{
    public interface INotificationRepo
    {
        Task<IEnumerable<Notification>> GetByUserIdAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task AddAsync(Notification notification);
        Task MarkAsReadAsync(int id);
        Task MarkAllAsReadAsync(string userId);
        Task DeleteAsync(int id);
    }
}
