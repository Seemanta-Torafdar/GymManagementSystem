using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepo _repo;
        public MembershipService(IMembershipRepo repo) { _repo = repo; }

        // Packages
        public async Task<IEnumerable<MembershipPackageDTO>> GetAllPackagesAsync() =>
            (await _repo.GetAllPackagesAsync()).Select(p => new MembershipPackageDTO { Id = p.Id, Name = p.Name, DurationDays = p.DurationDays, Price = p.Price, Benefits = p.Benefits, Description = p.Description, IsActive = p.IsActive });
        public async Task<MembershipPackageDTO?> GetPackageByIdAsync(int id)
        {
            var p = await _repo.GetPackageByIdAsync(id);
            return p == null ? null : new MembershipPackageDTO { Id = p.Id, Name = p.Name, DurationDays = p.DurationDays, Price = p.Price, Benefits = p.Benefits, Description = p.Description, IsActive = p.IsActive };
        }
        public async Task<bool> CreatePackageAsync(MembershipPackageDTO dto) { await _repo.AddPackageAsync(new MembershipPackage { Name = dto.Name, DurationDays = dto.DurationDays, Price = dto.Price, Benefits = dto.Benefits, Description = dto.Description }); return true; }
        public async Task<bool> UpdatePackageAsync(MembershipPackageDTO dto)
        {
            var p = await _repo.GetPackageByIdAsync(dto.Id);
            if (p == null) return false;
            p.Name = dto.Name; p.DurationDays = dto.DurationDays; p.Price = dto.Price; p.Benefits = dto.Benefits; p.Description = dto.Description;
            await _repo.UpdatePackageAsync(p); return true;
        }
        public async Task<bool> DeletePackageAsync(int id) { await _repo.DeletePackageAsync(id); return true; }

        // Purchases
        public async Task<IEnumerable<MembershipPurchaseDTO>> GetAllPurchasesAsync()
        {
            var purchases = await _repo.GetAllPurchasesAsync();
            return purchases.Select(mp => new MembershipPurchaseDTO
            {
                Id = mp.Id, MemberId = mp.MemberId,
                MemberName = mp.Member?.User != null ? $"{mp.Member.User.FirstName} {mp.Member.User.LastName}" : "",
                PackageId = mp.PackageId, PackageName = mp.Package?.Name ?? "",
                ShiftId = mp.ShiftId, ShiftName = mp.Shift?.ShiftName ?? "",
                YogaScheduleId = mp.YogaScheduleId, YogaClassName = mp.YogaSchedule?.ClassName,
                CardioScheduleId = mp.CardioScheduleId, CardioClassName = mp.CardioSchedule?.ClassName,
                StartDate = mp.StartDate, EndDate = mp.EndDate, IsActive = mp.IsActive, PaymentStatus = mp.PaymentStatus
            });
        }
        public async Task<MembershipPurchaseDTO?> GetActivePurchaseByMemberIdAsync(int memberId)
        {
            var mp = await _repo.GetActivePurchaseByMemberIdAsync(memberId);
            if (mp == null) return null;
            return new MembershipPurchaseDTO
            {
                Id = mp.Id, MemberId = mp.MemberId,
                PackageId = mp.PackageId, PackageName = mp.Package?.Name ?? "",
                ShiftId = mp.ShiftId, ShiftName = mp.Shift?.ShiftName ?? "",
                YogaScheduleId = mp.YogaScheduleId, YogaClassName = mp.YogaSchedule?.ClassName,
                CardioScheduleId = mp.CardioScheduleId, CardioClassName = mp.CardioSchedule?.ClassName,
                StartDate = mp.StartDate, EndDate = mp.EndDate, IsActive = mp.IsActive, PaymentStatus = mp.PaymentStatus
            };
        }
        public async Task<bool> PurchaseMembershipAsync(int memberId, int packageId, int shiftId, int? yogaId, int? cardioId)
        {
            var package = await _repo.GetPackageByIdAsync(packageId);
            if (package == null) return false;
            // Deactivate old
            var old = await _repo.GetActivePurchaseByMemberIdAsync(memberId);
            if (old != null) { old.IsActive = false; await _repo.UpdatePurchaseAsync(old); }
            var purchase = new MembershipPurchase
            {
                MemberId = memberId, PackageId = packageId, ShiftId = shiftId,
                YogaScheduleId = yogaId, CardioScheduleId = cardioId,
                StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(package.DurationDays),
                IsActive = true, PaymentStatus = "Pending"
            };
            await _repo.AddPurchaseAsync(purchase);
            return true;
        }
        public async Task<bool> RenewMembershipAsync(int memberId, int packageId, int shiftId)
        {
            var package = await _repo.GetPackageByIdAsync(packageId);
            if (package == null) return false;
            var old = await _repo.GetActivePurchaseByMemberIdAsync(memberId);
            if (old != null) { old.IsActive = false; await _repo.UpdatePurchaseAsync(old); }
            var purchase = new MembershipPurchase
            {
                MemberId = memberId, PackageId = packageId, ShiftId = shiftId,
                StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(package.DurationDays),
                IsActive = true, PaymentStatus = "Pending"
            };
            await _repo.AddPurchaseAsync(purchase);
            return true;
        }
        public async Task<bool> UpdatePaymentStatusAsync(int purchaseId, string status)
        {
            var p = await _repo.GetPurchaseByIdAsync(purchaseId);
            if (p == null) return false;
            p.PaymentStatus = status; await _repo.UpdatePurchaseAsync(p); return true;
        }

        // Shifts
        public async Task<IEnumerable<GymShiftDTO>> GetAllShiftsAsync() =>
            (await _repo.GetAllShiftsAsync()).Select(s => new GymShiftDTO { Id = s.Id, ShiftName = s.ShiftName, StartTime = s.StartTime, EndTime = s.EndTime, Capacity = s.Capacity, Description = s.Description });
        public async Task<bool> CreateShiftAsync(GymShiftDTO dto) { await _repo.AddShiftAsync(new GymShift { ShiftName = dto.ShiftName, StartTime = dto.StartTime, EndTime = dto.EndTime, Capacity = dto.Capacity, Description = dto.Description }); return true; }
        public async Task<bool> UpdateShiftAsync(GymShiftDTO dto)
        {
            var s = await _repo.GetShiftByIdAsync(dto.Id);
            if (s == null) return false;
            s.ShiftName = dto.ShiftName; s.StartTime = dto.StartTime; s.EndTime = dto.EndTime; s.Capacity = dto.Capacity; s.Description = dto.Description;
            await _repo.UpdateShiftAsync(s); return true;
        }
        public async Task<bool> DeleteShiftAsync(int id) { await _repo.DeleteShiftAsync(id); return true; }

        // Yoga
        public async Task<IEnumerable<YogaScheduleDTO>> GetAllYogaAsync() =>
            (await _repo.GetAllYogaAsync()).Select(y => new YogaScheduleDTO { Id = y.Id, ClassName = y.ClassName, DayOfWeek = y.DayOfWeek, StartTime = y.StartTime, EndTime = y.EndTime, Instructor = y.Instructor, Capacity = y.Capacity });
        public async Task<bool> CreateYogaAsync(YogaScheduleDTO dto) { await _repo.AddYogaAsync(new YogaSchedule { ClassName = dto.ClassName, DayOfWeek = dto.DayOfWeek, StartTime = dto.StartTime, EndTime = dto.EndTime, Instructor = dto.Instructor, Capacity = dto.Capacity }); return true; }
        public async Task<bool> UpdateYogaAsync(YogaScheduleDTO dto)
        {
            var y = await _repo.GetYogaByIdAsync(dto.Id);
            if (y == null) return false;
            y.ClassName = dto.ClassName; y.DayOfWeek = dto.DayOfWeek; y.StartTime = dto.StartTime; y.EndTime = dto.EndTime; y.Instructor = dto.Instructor; y.Capacity = dto.Capacity;
            await _repo.UpdateYogaAsync(y); return true;
        }
        public async Task<bool> DeleteYogaAsync(int id) { await _repo.DeleteYogaAsync(id); return true; }

        // Cardio
        public async Task<IEnumerable<CardioScheduleDTO>> GetAllCardioAsync() =>
            (await _repo.GetAllCardioAsync()).Select(c => new CardioScheduleDTO { Id = c.Id, ClassName = c.ClassName, DayOfWeek = c.DayOfWeek, StartTime = c.StartTime, EndTime = c.EndTime, EquipmentUsed = c.EquipmentUsed, Instructor = c.Instructor, Capacity = c.Capacity });
        public async Task<bool> CreateCardioAsync(CardioScheduleDTO dto) { await _repo.AddCardioAsync(new CardioSchedule { ClassName = dto.ClassName, DayOfWeek = dto.DayOfWeek, StartTime = dto.StartTime, EndTime = dto.EndTime, EquipmentUsed = dto.EquipmentUsed, Instructor = dto.Instructor, Capacity = dto.Capacity }); return true; }
        public async Task<bool> UpdateCardioAsync(CardioScheduleDTO dto)
        {
            var c = await _repo.GetCardioByIdAsync(dto.Id);
            if (c == null) return false;
            c.ClassName = dto.ClassName; c.DayOfWeek = dto.DayOfWeek; c.StartTime = dto.StartTime; c.EndTime = dto.EndTime; c.EquipmentUsed = dto.EquipmentUsed; c.Instructor = dto.Instructor; c.Capacity = dto.Capacity;
            await _repo.UpdateCardioAsync(c); return true;
        }
        public async Task<bool> DeleteCardioAsync(int id) { await _repo.DeleteCardioAsync(id); return true; }
    }
}
