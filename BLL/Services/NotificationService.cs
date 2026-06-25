using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepo _repo;
        private readonly IMemberRepo _memberRepo;
        private readonly IMembershipRepo _membershipRepo;

        public NotificationService(INotificationRepo repo, IMemberRepo memberRepo, IMembershipRepo membershipRepo)
        {
            _repo = repo; _memberRepo = memberRepo; _membershipRepo = membershipRepo;
        }

        public async Task<IEnumerable<NotificationDTO>> GetByUserIdAsync(string userId) =>
            (await _repo.GetByUserIdAsync(userId)).Select(n => new NotificationDTO
            { Id = n.Id, UserId = n.UserId, Title = n.Title, Message = n.Message, Type = n.Type, IsRead = n.IsRead, CreatedAt = n.CreatedAt });

        public async Task<int> GetUnreadCountAsync(string userId) => await _repo.GetUnreadCountAsync(userId);

        public async Task SendAsync(string userId, string title, string message, string type = "Info")
        {
            await _repo.AddAsync(new Notification { UserId = userId, Title = title, Message = message, Type = type });
        }

        public async Task MarkAsReadAsync(int id) => await _repo.MarkAsReadAsync(id);
        public async Task MarkAllAsReadAsync(string userId) => await _repo.MarkAllAsReadAsync(userId);
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task CheckAndSendExpiryNotificationsAsync()
        {
            var members = await _memberRepo.GetAllAsync();
            foreach (var member in members)
            {
                var purchase = await _membershipRepo.GetActivePurchaseByMemberIdAsync(member.Id);
                if (purchase == null) continue;
                int daysLeft = (purchase.EndDate - DateTime.Today).Days;
                if (daysLeft == 7)
                    await SendAsync(member.UserId, "Membership Expiring Soon", $"Your membership expires in 7 days on {purchase.EndDate:MMM dd, yyyy}. Please renew soon!", "Warning");
                else if (daysLeft == 3)
                    await SendAsync(member.UserId, "Urgent: Membership Expiring in 3 Days", $"Your membership expires in 3 days! Renew now to avoid interruption.", "Danger");
                else if (daysLeft == 0)
                    await SendAsync(member.UserId, "Membership Expired", "Your gym membership has expired. Please renew to continue using our facilities.", "Danger");
                else if (daysLeft < 0)
                {
                    purchase.IsActive = false;
                    await _membershipRepo.UpdatePurchaseAsync(purchase);
                }
            }
        }
    }
}
