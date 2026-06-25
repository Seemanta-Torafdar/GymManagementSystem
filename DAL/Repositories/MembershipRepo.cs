using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class MembershipRepo : IMembershipRepo
    {
        private readonly GymDbContext _context;
        public MembershipRepo(GymDbContext context) { _context = context; }

        // Packages
        public async Task<IEnumerable<MembershipPackage>> GetAllPackagesAsync() =>
            await _context.MembershipPackages.Where(p => p.IsActive).ToListAsync();
        public async Task<MembershipPackage?> GetPackageByIdAsync(int id) => await _context.MembershipPackages.FindAsync(id);
        public async Task AddPackageAsync(MembershipPackage p) { await _context.MembershipPackages.AddAsync(p); await _context.SaveChangesAsync(); }
        public async Task UpdatePackageAsync(MembershipPackage p) { _context.MembershipPackages.Update(p); await _context.SaveChangesAsync(); }
        public async Task DeletePackageAsync(int id)
        {
            var p = await _context.MembershipPackages.FindAsync(id);
            if (p != null) { p.IsActive = false; await _context.SaveChangesAsync(); }
        }

        // Purchases
        public async Task<IEnumerable<MembershipPurchase>> GetAllPurchasesAsync() =>
            await _context.MembershipPurchases.Include(mp => mp.Member).ThenInclude(m => m.User)
                .Include(mp => mp.Package).Include(mp => mp.Shift).ToListAsync();
        public async Task<MembershipPurchase?> GetPurchaseByIdAsync(int id) =>
            await _context.MembershipPurchases.Include(mp => mp.Member).ThenInclude(m => m.User)
                .Include(mp => mp.Package).Include(mp => mp.Shift)
                .Include(mp => mp.YogaSchedule).Include(mp => mp.CardioSchedule)
                .FirstOrDefaultAsync(mp => mp.Id == id);
        public async Task<MembershipPurchase?> GetActivePurchaseByMemberIdAsync(int memberId) =>
            await _context.MembershipPurchases.Include(mp => mp.Package).Include(mp => mp.Shift)
                .Include(mp => mp.YogaSchedule).Include(mp => mp.CardioSchedule)
                .OrderByDescending(mp => mp.StartDate)
                .FirstOrDefaultAsync(mp => mp.MemberId == memberId && mp.IsActive);
        public async Task AddPurchaseAsync(MembershipPurchase purchase) { await _context.MembershipPurchases.AddAsync(purchase); await _context.SaveChangesAsync(); }
        public async Task UpdatePurchaseAsync(MembershipPurchase purchase) { _context.MembershipPurchases.Update(purchase); await _context.SaveChangesAsync(); }

        // Shifts
        public async Task<IEnumerable<GymShift>> GetAllShiftsAsync() => await _context.GymShifts.Where(s => s.IsActive).ToListAsync();
        public async Task<GymShift?> GetShiftByIdAsync(int id) => await _context.GymShifts.FindAsync(id);
        public async Task AddShiftAsync(GymShift shift) { await _context.GymShifts.AddAsync(shift); await _context.SaveChangesAsync(); }
        public async Task UpdateShiftAsync(GymShift shift) { _context.GymShifts.Update(shift); await _context.SaveChangesAsync(); }
        public async Task DeleteShiftAsync(int id)
        {
            var s = await _context.GymShifts.FindAsync(id);
            if (s != null) { s.IsActive = false; await _context.SaveChangesAsync(); }
        }

        // Yoga
        public async Task<IEnumerable<YogaSchedule>> GetAllYogaAsync() => await _context.YogaSchedules.Where(y => y.IsActive).ToListAsync();
        public async Task<YogaSchedule?> GetYogaByIdAsync(int id) => await _context.YogaSchedules.FindAsync(id);
        public async Task AddYogaAsync(YogaSchedule yoga) { await _context.YogaSchedules.AddAsync(yoga); await _context.SaveChangesAsync(); }
        public async Task UpdateYogaAsync(YogaSchedule yoga) { _context.YogaSchedules.Update(yoga); await _context.SaveChangesAsync(); }
        public async Task DeleteYogaAsync(int id)
        {
            var y = await _context.YogaSchedules.FindAsync(id);
            if (y != null) { y.IsActive = false; await _context.SaveChangesAsync(); }
        }

        // Cardio
        public async Task<IEnumerable<CardioSchedule>> GetAllCardioAsync() => await _context.CardioSchedules.Where(c => c.IsActive).ToListAsync();
        public async Task<CardioSchedule?> GetCardioByIdAsync(int id) => await _context.CardioSchedules.FindAsync(id);
        public async Task AddCardioAsync(CardioSchedule cardio) { await _context.CardioSchedules.AddAsync(cardio); await _context.SaveChangesAsync(); }
        public async Task UpdateCardioAsync(CardioSchedule cardio) { _context.CardioSchedules.Update(cardio); await _context.SaveChangesAsync(); }
        public async Task DeleteCardioAsync(int id)
        {
            var c = await _context.CardioSchedules.FindAsync(id);
            if (c != null) { c.IsActive = false; await _context.SaveChangesAsync(); }
        }

        // Stats
        public async Task<int> GetActiveMembershipsCountAsync() =>
            await _context.MembershipPurchases.CountAsync(mp => mp.IsActive && mp.EndDate >= DateTime.Today);
        public async Task<int> GetExpiredMembershipsCountAsync() =>
            await _context.MembershipPurchases.CountAsync(mp => mp.EndDate < DateTime.Today);
    }
}
