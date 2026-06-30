using DAL.EF.Models;

namespace DAL.Interfaces
{
    public interface ITrainerRepo
    {
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task<Trainer?> GetByIdAsync(int id);
        Task<Trainer?> GetByUserIdAsync(string userId);
        Task AddAsync(Trainer trainer);
        Task UpdateAsync(Trainer trainer);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<TrainerAssignment>> GetAssignmentsByTrainerIdAsync(int trainerId);
        Task<TrainerAssignment?> GetAssignmentByIdAsync(int id);
        Task<TrainerAssignment?> GetActiveAssignmentByMemberIdAsync(int memberId);
        Task UpdateTrainerAssignmentAsync(TrainerAssignment assignment);
    }
}
