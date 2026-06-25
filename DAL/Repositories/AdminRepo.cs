using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AdminRepo : IAdminRepo
    {
        private readonly GymDbContext _context;
        public AdminRepo(GymDbContext context) { _context = context; }

        public async Task<GymSetting?> GetGymSettingsAsync() => await _context.GymSettings.FirstOrDefaultAsync();
        public async Task UpdateGymSettingsAsync(GymSetting settings) { _context.GymSettings.Update(settings); await _context.SaveChangesAsync(); }

        public async Task<IEnumerable<TrainerReview>> GetAllReviewsAsync() =>
            await _context.TrainerReviews.Include(r => r.Trainer).ThenInclude(t => t.User)
                .Include(r => r.Member).ThenInclude(m => m.User)
                .OrderByDescending(r => r.ReviewDate).ToListAsync();

        public async Task<IEnumerable<TrainerReview>> GetReviewsByTrainerIdAsync(int trainerId) =>
            await _context.TrainerReviews.Include(r => r.Member).ThenInclude(m => m.User)
                .Where(r => r.TrainerId == trainerId && r.IsApproved)
                .OrderByDescending(r => r.ReviewDate).ToListAsync();

        public async Task AddReviewAsync(TrainerReview review) { await _context.TrainerReviews.AddAsync(review); await _context.SaveChangesAsync(); }

        public async Task DeleteReviewAsync(int id)
        {
            var r = await _context.TrainerReviews.FindAsync(id);
            if (r != null) { _context.TrainerReviews.Remove(r); await _context.SaveChangesAsync(); }
        }

        public async Task<double> GetAverageRatingByTrainerIdAsync(int trainerId)
        {
            var reviews = await _context.TrainerReviews.Where(r => r.TrainerId == trainerId && r.IsApproved).ToListAsync();
            return reviews.Any() ? reviews.Average(r => r.Rating) : 0;
        }

        public async Task<bool> HasMemberReviewedTrainerAsync(int memberId, int trainerId) =>
            await _context.TrainerReviews.AnyAsync(r => r.MemberId == memberId && r.TrainerId == trainerId);
    }
}
