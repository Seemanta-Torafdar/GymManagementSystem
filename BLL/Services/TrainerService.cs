using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepo _trainerRepo;
        private readonly IAdminRepo _adminRepo;
        private readonly UserManager<User> _userManager;

        public TrainerService(ITrainerRepo trainerRepo, IAdminRepo adminRepo, UserManager<User> userManager)
        {
            _trainerRepo = trainerRepo;
            _adminRepo = adminRepo;
            _userManager = userManager;
        }

        public async Task<IEnumerable<TrainerDTO>> GetAllAsync()
        {
            var trainers = await _trainerRepo.GetAllAsync();
            var dtos = new List<TrainerDTO>();
            foreach (var t in trainers)
            {
                var avg = await _adminRepo.GetAverageRatingByTrainerIdAsync(t.Id);
                dtos.Add(MapToDTO(t, avg));
            }
            return dtos;
        }

        public async Task<TrainerDTO?> GetByIdAsync(int id)
        {
            var trainer = await _trainerRepo.GetByIdAsync(id);
            if (trainer == null) return null;
            var avg = await _adminRepo.GetAverageRatingByTrainerIdAsync(id);
            return MapToDTO(trainer, avg);
        }

        public async Task<TrainerDTO?> GetByUserIdAsync(string userId)
        {
            var trainer = await _trainerRepo.GetByUserIdAsync(userId);
            if (trainer == null) return null;
            var avg = await _adminRepo.GetAverageRatingByTrainerIdAsync(trainer.Id);
            return MapToDTO(trainer, avg);
        }

        public async Task<bool> CreateAsync(TrainerCreateDTO dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Role = "Trainer",
                EmailConfirmed = true,
                IsActive = true,
                ProfilePhoto = dto.ProfilePhoto
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return false;
            await _userManager.AddToRoleAsync(user, "Trainer");
            var trainer = new Trainer
            {
                UserId = user.Id,
                Specialization = dto.Specialization,
                Experience = dto.Experience,
                MonthlySalary = dto.MonthlySalary,
                TrainingCharge = dto.TrainingCharge,
                Bio = dto.Bio,
                Certifications = dto.Certifications,
                DateOfBirth = dto.DateOfBirth,
                Phone = dto.Phone,
                TrainingTime = dto.TrainingTime
            };
            await _trainerRepo.AddAsync(trainer);
            
            // Generate unique GymId using the newly created Id
            trainer.GymId = "T" + (100000 + trainer.Id).ToString();
            await _trainerRepo.UpdateAsync(trainer);
            
            return true;
        }

        public async Task<bool> UpdateAsync(TrainerEditDTO dto)
        {
            var trainer = await _trainerRepo.GetByIdAsync(dto.Id);
            if (trainer == null) return false;
            trainer.Specialization = dto.Specialization;
            trainer.Experience = dto.Experience;
            trainer.MonthlySalary = dto.MonthlySalary;
            trainer.TrainingCharge = dto.TrainingCharge;
            trainer.Bio = dto.Bio;
            trainer.Certifications = dto.Certifications;
            trainer.IsAvailable = dto.IsAvailable;
            trainer.DateOfBirth = dto.DateOfBirth;
            trainer.Phone = dto.Phone;
            trainer.TrainingTime = dto.TrainingTime;
            if (!string.IsNullOrEmpty(dto.ProfilePhoto))
            {
                var user = await _userManager.FindByIdAsync(trainer.UserId);
                if (user != null)
                {
                    user.ProfilePhoto = dto.ProfilePhoto;
                    await _userManager.UpdateAsync(user);
                }
            }
            await _trainerRepo.UpdateAsync(trainer);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trainer = await _trainerRepo.GetByIdAsync(id);
            if (trainer == null) return false;
            var user = await _userManager.FindByIdAsync(trainer.UserId);
            if (user != null) { user.IsActive = false; await _userManager.UpdateAsync(user); }
            await _trainerRepo.DeleteAsync(id);
            return true;
        }

        public async Task<bool> UpdateProfilePhotoAsync(string userId, string photoPath)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            user.ProfilePhoto = photoPath;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> AssignMemberAsync(int trainerId, int memberId, string? workoutPlan, string? notes)
        {
            var trainer = await _trainerRepo.GetByIdAsync(trainerId);
            if (trainer == null) return false;
            trainer.TrainerAssignments.Add(new TrainerAssignment
            {
                TrainerId = trainerId,
                MemberId = memberId,
                WorkoutPlan = workoutPlan,
                TrainingNotes = notes,
                AssignedDate = DateTime.Now,
                IsActive = true
            });
            await _trainerRepo.UpdateAsync(trainer);
            return true;
        }

        public async Task<bool> RemoveTrainerAssignmentAsync(int memberId)
        {
            var trainers = await _trainerRepo.GetAllAsync();
            var assignmentsUpdated = false;
            foreach (var trainer in trainers)
            {
                var assignments = trainer.TrainerAssignments?.Where(a => a.MemberId == memberId && a.IsActive).ToList();
                if (assignments != null && assignments.Any())
                {
                    foreach (var assignment in assignments)
                    {
                        assignment.IsActive = false;
                    }
                    await _trainerRepo.UpdateAsync(trainer);
                    assignmentsUpdated = true;
                }
            }
            return assignmentsUpdated;
        }

        public async Task<bool> UpdateWorkoutPlanAsync(int assignmentId, string workoutPlan, string notes)
        {
            var assignment = await _trainerRepo.GetAssignmentByIdAsync(assignmentId);
            if (assignment == null) return false;
            
            // If the workoutPlan parameter is empty but they didn't upload a new file, we shouldn't wipe out the existing one unless they cleared it. 
            // In the controller, we passed `workoutPlanPath`. We should just set it directly.
            assignment.WorkoutPlan = workoutPlan;
            assignment.TrainingNotes = notes;
            
            await _trainerRepo.UpdateTrainerAssignmentAsync(assignment);
            return true;
        }

        private TrainerDTO MapToDTO(Trainer t, double avgRating) => new()
        {
            Id = t.Id,
            GymId = t.GymId,
            UserId = t.UserId,
            FirstName = t.User?.FirstName ?? "",
            LastName = t.User?.LastName ?? "",
            Email = t.User?.Email ?? "",
            Specialization = t.Specialization,
            Experience = t.Experience,
            MonthlySalary = t.MonthlySalary,
            TrainingCharge = t.TrainingCharge,
            Bio = t.Bio,
            Certifications = t.Certifications,
            ProfilePhoto = t.User?.ProfilePhoto,
            IsAvailable = t.IsAvailable,
            JoinDate = t.JoinDate,
            DateOfBirth = t.DateOfBirth,
            Phone = t.Phone,
            TrainingTime = t.TrainingTime,
            AssignedMembersCount = t.TrainerAssignments?.Count(ta => ta.IsActive) ?? 0,
            AverageRating = Math.Round(avgRating, 1),
            TotalReviews = t.TrainerReviews?.Count ?? 0,
            Assignments = t.TrainerAssignments?.Where(ta => ta.IsActive).Select(ta => 
            {
                var activePurchase = ta.Member?.MembershipPurchases?.FirstOrDefault(mp => mp.IsActive);
                var workoutTime = activePurchase != null ? activePurchase.Shift?.ShiftName : "Not Set";
                
                return new TrainerAssignmentDTO
                {
                    Id = ta.Id,
                    MemberId = ta.MemberId,
                    MemberGymId = ta.Member?.GymId ?? "",
                    MemberName = (ta.Member?.User?.FirstName + " " + ta.Member?.User?.LastName).Trim(),
                    MemberPhoto = ta.Member?.User?.ProfilePhoto,
                    MemberAge = ta.Member?.DateOfBirth != null && ta.Member.DateOfBirth != DateTime.MinValue ? DateTime.Today.Year - ta.Member.DateOfBirth.Year : 0,
                    MemberBloodGroup = ta.Member?.BloodGroup,
                    MemberEmail = ta.Member?.User?.Email ?? "",
                    MemberPhone = ta.Member?.Phone ?? "",
                    MemberWorkoutTime = workoutTime,
                    WorkoutPlan = ta.WorkoutPlan,
                    TrainingNotes = ta.TrainingNotes,
                    AssignedDate = ta.AssignedDate,
                    IsActive = ta.IsActive
                };
            }).ToList() ?? new List<TrainerAssignmentDTO>()
        };
    }
}
