using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TrainerRepo : ITrainerRepo
    {
        private readonly GymDbContext _context;
        public TrainerRepo(GymDbContext context) { _context = context; }

        public async Task<IEnumerable<Trainer>> GetAllAsync() =>
            await _context.Trainers.Include(t => t.User)
                .Include(t => t.TrainerAssignments)
                .Include(t => t.TrainerReviews)
                .ToListAsync();

        public async Task<Trainer?> GetByIdAsync(int id) =>
            await _context.Trainers.Include(t => t.User)
                .Include(t => t.TrainerAssignments).ThenInclude(ta => ta.Member).ThenInclude(m => m.User)
                .Include(t => t.TrainerReviews).ThenInclude(r => r.Member).ThenInclude(m => m.User)
                .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<Trainer?> GetByUserIdAsync(string userId) =>
            await _context.Trainers.Include(t => t.User)
                .Include(t => t.TrainerAssignments).ThenInclude(ta => ta.Member).ThenInclude(m => m.User)
                .Include(t => t.TrainerReviews)
                .FirstOrDefaultAsync(t => t.UserId == userId);

        public async Task AddAsync(Trainer trainer) { await _context.Trainers.AddAsync(trainer); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Trainer trainer) { _context.Trainers.Update(trainer); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null) { _context.Trainers.Remove(trainer); await _context.SaveChangesAsync(); }
        }
        public async Task<int> GetTotalCountAsync() => await _context.Trainers.CountAsync();
        public async Task<IEnumerable<TrainerAssignment>> GetAssignmentsByTrainerIdAsync(int trainerId) =>
            await _context.TrainerAssignments.Include(ta => ta.Member).ThenInclude(m => m.User)
                .Where(ta => ta.TrainerId == trainerId && ta.IsActive)
                .ToListAsync();
    }
}
