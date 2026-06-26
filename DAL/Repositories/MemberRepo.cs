using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class MemberRepo : IMemberRepo
    {
        private readonly GymDbContext _context;
        public MemberRepo(GymDbContext context) { _context = context; }

        public async Task<IEnumerable<Member>> GetAllAsync() =>
            await _context.Members.Include(m => m.User)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Package)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Shift)
                .Include(m => m.TrainerAssignments).ThenInclude(ta => ta.Trainer).ThenInclude(t => t.User)
                .ToListAsync();

        public async Task<Member?> GetByIdAsync(int id) =>
            await _context.Members.Include(m => m.User)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Package)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Shift)
                .Include(m => m.TrainerAssignments).ThenInclude(ta => ta.Trainer).ThenInclude(t => t.User)
                .Include(m => m.Payments)
                .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<Member?> GetByUserIdAsync(string userId) =>
            await _context.Members.Include(m => m.User)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Package)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Shift)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.YogaSchedule)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.CardioSchedule)
                .Include(m => m.TrainerAssignments).ThenInclude(ta => ta.Trainer).ThenInclude(t => t.User)
                .Include(m => m.Payments)
                .FirstOrDefaultAsync(m => m.UserId == userId);

        public async Task AddAsync(Member member) { await _context.Members.AddAsync(member); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Member member) { _context.Members.Update(member); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null) { _context.Members.Remove(member); await _context.SaveChangesAsync(); }
        }
        public async Task<int> GetTotalCountAsync() => await _context.Members.CountAsync();
        public async Task<IEnumerable<Member>> SearchAsync(string query) =>
            await _context.Members.Include(m => m.User)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Package)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Shift)
                .Include(m => m.TrainerAssignments).ThenInclude(ta => ta.Trainer).ThenInclude(t => t.User)
                .Where(m => m.User.FirstName.Contains(query) || m.User.LastName.Contains(query) || m.User.Email!.Contains(query) || m.Phone.Contains(query))
                .ToListAsync();
    }
}
