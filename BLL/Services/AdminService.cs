using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        private readonly IMemberRepo _memberRepo;
        private readonly ITrainerRepo _trainerRepo;
        private readonly IMembershipRepo _membershipRepo;
        private readonly IPaymentRepo _paymentRepo;
        private readonly IEquipmentRepo _equipmentRepo;

        public AdminService(IAdminRepo adminRepo, IMemberRepo memberRepo, ITrainerRepo trainerRepo,
            IMembershipRepo membershipRepo, IPaymentRepo paymentRepo, IEquipmentRepo equipmentRepo)
        {
            _adminRepo = adminRepo; _memberRepo = memberRepo; _trainerRepo = trainerRepo;
            _membershipRepo = membershipRepo; _paymentRepo = paymentRepo; _equipmentRepo = equipmentRepo;
        }

        public async Task<AdminDashboardDTO> GetDashboardDataAsync()
        {
            var now = DateTime.Now;
            var allPayments = await _paymentRepo.GetAllAsync();
            var allTrainerPayments = await _paymentRepo.GetAllTrainerPaymentsAsync();
            var allInventory = await _equipmentRepo.GetAllInventoryAsync();
            var recentMembers = (await _memberRepo.GetAllAsync()).OrderByDescending(m => m.JoinDate).Take(5).ToList();

            return new AdminDashboardDTO
            {
                TotalMembers = await _memberRepo.GetTotalCountAsync(),
                TotalTrainers = await _trainerRepo.GetTotalCountAsync(),
                ActiveMemberships = await _membershipRepo.GetActiveMembershipsCountAsync(),
                ExpiredMemberships = await _membershipRepo.GetExpiredMembershipsCountAsync(),
                PendingPayments = await _paymentRepo.GetPendingCountAsync(),
                MonthlyRevenue = await _paymentRepo.GetMonthlyRevenueAsync(now.Month, now.Year),
                PendingTrainerPayments = allTrainerPayments.Count(tp => tp.PaymentStatus == "Unpaid" || tp.PaymentStatus == "Partial Paid"),
                LowStockEquipment = allInventory.Count(i => i.StockStatus == "Low" || i.StockStatus == "OutOfStock"),
                RecentPayments = allPayments.Take(5).Select(p => new PaymentDTO
                {
                    Id = p.Id, MemberId = p.MemberId,
                    MemberName = p.Member?.User != null ? $"{p.Member.User.FirstName} {p.Member.User.LastName}" : "",
                    TotalAmount = p.TotalAmount, AmountPaid = p.AmountPaid, PaymentStatus = p.PaymentStatus, PaymentDate = p.PaymentDate, DueDate = p.DueDate
                }).ToList(),
                RecentMembers = recentMembers.Select(m => new MemberDTO
                {
                    Id = m.Id, FirstName = m.User?.FirstName ?? "", LastName = m.User?.LastName ?? "",
                    Email = m.User?.Email ?? "", Phone = m.Phone, JoinDate = m.JoinDate
                }).ToList(),
                PendingTrainerPaymentList = allTrainerPayments.Where(tp => tp.PaymentStatus != "Paid").Select(tp => new TrainerPaymentDTO
                {
                    Id = tp.Id, TrainerId = tp.TrainerId,
                    TrainerName = tp.Trainer?.User != null ? $"{tp.Trainer.User.FirstName} {tp.Trainer.User.LastName}" : "",
                    Month = tp.Month, Year = tp.Year, TotalSalary = tp.TotalSalary, AmountPaid = tp.AmountPaid, PaymentStatus = tp.PaymentStatus
                }).ToList()
            };
        }

        public async Task<GymSettingDTO?> GetGymSettingsAsync()
        {
            var s = await _adminRepo.GetGymSettingsAsync();
            if (s == null) return null;
            return new GymSettingDTO { Id = s.Id, GymName = s.GymName, LogoPath = s.LogoPath, Phone = s.Phone, Email = s.Email, Address = s.Address, AboutUs = s.AboutUs, FacebookUrl = s.FacebookUrl, InstagramUrl = s.InstagramUrl, TwitterUrl = s.TwitterUrl, YouTubeUrl = s.YouTubeUrl, BannerImage1 = s.BannerImage1, BannerImage2 = s.BannerImage2, BannerImage3 = s.BannerImage3, HeroTagline = s.HeroTagline };
        }

        public async Task<bool> UpdateGymSettingsAsync(GymSettingDTO dto)
        {
            var s = await _adminRepo.GetGymSettingsAsync();
            if (s == null) return false;
            s.GymName = dto.GymName; s.Phone = dto.Phone; s.Email = dto.Email; s.Address = dto.Address;
            s.AboutUs = dto.AboutUs; s.FacebookUrl = dto.FacebookUrl; s.InstagramUrl = dto.InstagramUrl;
            s.TwitterUrl = dto.TwitterUrl; s.YouTubeUrl = dto.YouTubeUrl; s.HeroTagline = dto.HeroTagline;
            if (dto.LogoPath != null) s.LogoPath = dto.LogoPath;
            if (dto.BannerImage1 != null) s.BannerImage1 = dto.BannerImage1;
            if (dto.BannerImage2 != null) s.BannerImage2 = dto.BannerImage2;
            if (dto.BannerImage3 != null) s.BannerImage3 = dto.BannerImage3;
            s.UpdatedAt = DateTime.Now;
            await _adminRepo.UpdateGymSettingsAsync(s);
            return true;
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync() =>
            (await _adminRepo.GetAllReviewsAsync()).Select(r => new ReviewDTO
            {
                Id = r.Id, TrainerId = r.TrainerId,
                TrainerName = r.Trainer?.User != null ? $"{r.Trainer.User.FirstName} {r.Trainer.User.LastName}" : "",
                MemberId = r.MemberId,
                MemberName = r.Member?.User != null ? $"{r.Member.User.FirstName} {r.Member.User.LastName}" : "",
                Rating = r.Rating, Comment = r.Comment, ReviewDate = r.ReviewDate
            });

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByTrainerIdAsync(int trainerId) =>
            (await _adminRepo.GetReviewsByTrainerIdAsync(trainerId)).Select(r => new ReviewDTO
            {
                Id = r.Id, TrainerId = r.TrainerId, MemberId = r.MemberId,
                MemberName = r.Member?.User != null ? $"{r.Member.User.FirstName} {r.Member.User.LastName}" : "",
                Rating = r.Rating, Comment = r.Comment, ReviewDate = r.ReviewDate
            });

        public async Task<bool> AddReviewAsync(int memberId, int trainerId, int rating, string? comment)
        {
            if (rating < 1 || rating > 5) return false;
            await _adminRepo.AddReviewAsync(new TrainerReview
            { MemberId = memberId, TrainerId = trainerId, Rating = rating, Comment = comment, ReviewDate = DateTime.Now });
            return true;
        }

        public async Task<bool> DeleteReviewAsync(int id) { await _adminRepo.DeleteReviewAsync(id); return true; }
        public async Task<bool> HasMemberReviewedTrainerAsync(int memberId, int trainerId) =>
            await _adminRepo.HasMemberReviewedTrainerAsync(memberId, trainerId);
    }
}
