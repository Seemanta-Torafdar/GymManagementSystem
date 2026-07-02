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
        public async Task<IEnumerable<Member>> SearchAsync(string query)
        {
            var q = query.Trim().ToLower();
            var parts = q.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var dbQuery = _context.Members.Include(m => m.User)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Package)
                .Include(m => m.MembershipPurchases).ThenInclude(mp => mp.Shift)
                .Include(m => m.TrainerAssignments).ThenInclude(ta => ta.Trainer).ThenInclude(t => t.User)
                .AsQueryable();

            if (parts.Length == 2)
            {
                var p1 = parts[0];
                var p2 = parts[1];
                dbQuery = dbQuery.Where(m => 
                    (m.User.Email != null && m.User.Email.ToLower() == q) || 
                    (m.GymId != null && m.GymId.ToLower() == q) ||
                    (m.Phone != null && m.Phone.Contains(q)) ||
                    (m.User.FirstName.ToLower().Contains(p1) && m.User.LastName.ToLower().Contains(p2)) ||
                    (m.User.FirstName.ToLower().Contains(p2) && m.User.LastName.ToLower().Contains(p1)) ||
                    (m.User.FirstName + " " + m.User.LastName).ToLower().Contains(q)
                );
            }
            else
            {
                dbQuery = dbQuery.Where(m => 
                    m.User.FirstName.ToLower().Contains(q) || 
                    m.User.LastName.ToLower().Contains(q) || 
                    (m.User.FirstName + " " + m.User.LastName).ToLower().Contains(q) || 
                    (m.User.Email != null && m.User.Email.ToLower() == q) || 
                    (m.Phone != null && m.Phone.Contains(q)) || 
                    (m.GymId != null && m.GymId.ToLower() == q));
            }
            return await dbQuery.ToListAsync();
        }
    }
}
