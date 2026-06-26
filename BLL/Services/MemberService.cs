using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepo _memberRepo;
        private readonly ITrainerRepo _trainerRepo;
        private readonly IMembershipRepo _membershipRepo;
        private readonly UserManager<User> _userManager;

        public MemberService(IMemberRepo memberRepo, ITrainerRepo trainerRepo, IMembershipRepo membershipRepo, UserManager<User> userManager)
        {
            _memberRepo = memberRepo;
            _trainerRepo = trainerRepo;
            _membershipRepo = membershipRepo;
            _userManager = userManager;
        }

        public async Task<IEnumerable<MemberDTO>> GetAllAsync()
        {
            var members = await _memberRepo.GetAllAsync();
            return members.Select(MapToDTO);
        }

        public async Task<MemberDTO?> GetByIdAsync(int id)
        {
            var member = await _memberRepo.GetByIdAsync(id);
            return member == null ? null : MapToDTO(member);
        }

        public async Task<MemberDTO?> GetByUserIdAsync(string userId)
        {
            var member = await _memberRepo.GetByUserIdAsync(userId);
            return member == null ? null : MapToDTO(member);
        }

        public async Task<(bool Success, IEnumerable<string> Errors)> CreateAsync(MemberCreateDTO dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Role = "Member",
                EmailConfirmed = true,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return (false, result.Errors.Select(e => e.Description));
            await _userManager.AddToRoleAsync(user, "Member");

            var member = new Member
            {
                UserId = user.Id,
                Phone = dto.Phone,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                BloodGroup = dto.BloodGroup,
                EmergencyContact = dto.EmergencyContact,
                EmergencyPhone = dto.EmergencyPhone
            };
            await _memberRepo.AddAsync(member);

            // Create membership purchase
            var package = await _membershipRepo.GetPackageByIdAsync(dto.PackageId);
            if (package != null)
            {
                var purchase = new MembershipPurchase
                {
                    MemberId = member.Id,
                    PackageId = dto.PackageId,
                    ShiftId = dto.ShiftId,
                    YogaScheduleId = dto.YogaScheduleId,
                    CardioScheduleId = dto.CardioScheduleId,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(package.DurationDays),
                    IsActive = true,
                    PaymentStatus = "Pending"
                };
                await _membershipRepo.AddPurchaseAsync(purchase);
            }

            // Assign trainer if selected
            if (dto.TrainerId.HasValue)
            {
                var trainer = await _trainerRepo.GetByIdAsync(dto.TrainerId.Value);
                if (trainer != null)
                {
                    trainer.TrainerAssignments.Add(new TrainerAssignment
                    {
                        TrainerId = dto.TrainerId.Value,
                        MemberId = member.Id,
                        AssignedDate = DateTime.Now,
                        IsActive = true
                    });
                    await _trainerRepo.UpdateAsync(trainer);
                }
            }
            return (true, Enumerable.Empty<string>());
        }

        public async Task<bool> UpdateAsync(MemberEditDTO dto)
        {
            var member = await _memberRepo.GetByIdAsync(dto.Id);
            if (member == null) return false;
            member.Phone = dto.Phone;
            member.Gender = dto.Gender;
            member.DateOfBirth = dto.DateOfBirth;
            member.Address = dto.Address;
            member.BloodGroup = dto.BloodGroup;
            member.EmergencyContact = dto.EmergencyContact;
            member.EmergencyPhone = dto.EmergencyPhone;
            member.MedicalNotes = dto.MedicalNotes;
            member.User.FirstName = dto.FirstName;
            member.User.LastName = dto.LastName;
            await _memberRepo.UpdateAsync(member);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _memberRepo.GetByIdAsync(id);
            if (member == null) return false;
            var user = await _userManager.FindByIdAsync(member.UserId);
            if (user != null) { user.IsActive = false; await _userManager.UpdateAsync(user); }
            await _memberRepo.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<MemberDTO>> SearchAsync(string query)
        {
            var members = await _memberRepo.SearchAsync(query);
            return members.Select(MapToDTO);
        }

        public async Task<bool> UpdateProfilePhotoAsync(string userId, string photoPath)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            user.ProfilePhoto = photoPath;
            await _userManager.UpdateAsync(user);
            return true;
        }

        private MemberDTO MapToDTO(Member m)
        {
            var activePurchase = m.MembershipPurchases?.OrderByDescending(p => p.StartDate).FirstOrDefault(p => p.IsActive);
            var activeAssignment = m.TrainerAssignments?.FirstOrDefault(ta => ta.IsActive);
            return new MemberDTO
            {
                Id = m.Id,
                UserId = m.UserId,
                FirstName = m.User?.FirstName ?? "",
                LastName = m.User?.LastName ?? "",
                Email = m.User?.Email ?? "",
                Phone = m.Phone,
                Gender = m.Gender,
                DateOfBirth = m.DateOfBirth,
                Address = m.Address,
                BloodGroup = m.BloodGroup,
                EmergencyContact = m.EmergencyContact,
                EmergencyPhone = m.EmergencyPhone,
                MedicalNotes = m.MedicalNotes,
                ProfilePhoto = m.User?.ProfilePhoto,
                JoinDate = m.JoinDate,
                IsActive = m.User?.IsActive ?? false,
                ActivePackageName = activePurchase?.Package?.Name,
                MembershipStartDate = activePurchase?.StartDate,
                MembershipEndDate = activePurchase?.EndDate,
                RemainingDays = activePurchase != null ? Math.Max(0, (activePurchase.EndDate - DateTime.Today).Days) : null,
                PaymentStatus = activePurchase?.PaymentStatus,
                ShiftName = activePurchase?.Shift?.ShiftName,
                YogaClassName = activePurchase?.YogaSchedule?.ClassName,
                YogaTimeRange = activePurchase?.YogaSchedule != null ? $"{activePurchase.YogaSchedule.DayOfWeek} {activePurchase.YogaSchedule.StartTime:hh\\:mm} - {activePurchase.YogaSchedule.EndTime:hh\\:mm}" : null,
                CardioClassName = activePurchase?.CardioSchedule?.ClassName,
                CardioTimeRange = activePurchase?.CardioSchedule != null ? $"{activePurchase.CardioSchedule.DayOfWeek} {activePurchase.CardioSchedule.StartTime:hh\\:mm} - {activePurchase.CardioSchedule.EndTime:hh\\:mm}" : null,
                AssignedTrainerId = activeAssignment?.TrainerId,
                AssignedTrainerName = activeAssignment?.Trainer?.User != null ? $"{activeAssignment.Trainer.User.FirstName} {activeAssignment.Trainer.User.LastName}" : null
            };
        }
    }
}
