using DAL.EF.Models;

namespace DAL.Interfaces
{
    public interface IMembershipRepo
    {
        Task<IEnumerable<MembershipPackage>> GetAllPackagesAsync();
        Task<MembershipPackage?> GetPackageByIdAsync(int id);
        Task AddPackageAsync(MembershipPackage package);
        Task UpdatePackageAsync(MembershipPackage package);
        Task DeletePackageAsync(int id);

        Task<IEnumerable<MembershipPurchase>> GetAllPurchasesAsync();
        Task<MembershipPurchase?> GetPurchaseByIdAsync(int id);
        Task<MembershipPurchase?> GetActivePurchaseByMemberIdAsync(int memberId);
        Task AddPurchaseAsync(MembershipPurchase purchase);
        Task UpdatePurchaseAsync(MembershipPurchase purchase);

        Task<IEnumerable<GymShift>> GetAllShiftsAsync();
        Task<GymShift?> GetShiftByIdAsync(int id);
        Task AddShiftAsync(GymShift shift);
        Task UpdateShiftAsync(GymShift shift);
        Task DeleteShiftAsync(int id);

        Task<IEnumerable<YogaSchedule>> GetAllYogaAsync();
        Task<IEnumerable<CardioSchedule>> GetAllCardioAsync();
        Task<YogaSchedule?> GetYogaByIdAsync(int id);
        Task<CardioSchedule?> GetCardioByIdAsync(int id);
        Task AddYogaAsync(YogaSchedule yoga);
        Task UpdateYogaAsync(YogaSchedule yoga);
        Task DeleteYogaAsync(int id);
        Task AddCardioAsync(CardioSchedule cardio);
        Task UpdateCardioAsync(CardioSchedule cardio);
        Task DeleteCardioAsync(int id);

        Task<int> GetActiveMembershipsCountAsync();
        Task<int> GetExpiredMembershipsCountAsync();
    }
}
