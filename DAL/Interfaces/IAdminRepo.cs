using DAL.EF.Models;

namespace DAL.Interfaces
{
    public interface IAdminRepo
    {
        Task<GymSetting?> GetGymSettingsAsync();
        Task UpdateGymSettingsAsync(GymSetting settings);
        Task<IEnumerable<TrainerReview>> GetAllReviewsAsync();
        Task<IEnumerable<TrainerReview>> GetReviewsByTrainerIdAsync(int trainerId);
        Task AddReviewAsync(TrainerReview review);
        Task DeleteReviewAsync(int id);
        Task<double> GetAverageRatingByTrainerIdAsync(int trainerId);
        Task<bool> HasMemberReviewedTrainerAsync(int memberId, int trainerId);
    }
}
