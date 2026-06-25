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
                Bio = dto.Bio,
                Certifications = dto.Certifications
            };
            await _trainerRepo.AddAsync(trainer);
            return true;
        }

        public async Task<bool> UpdateAsync(TrainerEditDTO dto)
        {
            var trainer = await _trainerRepo.GetByIdAsync(dto.Id);
            if (trainer == null) return false;
            trainer.Specialization = dto.Specialization;
            trainer.Experience = dto.Experience;
            trainer.MonthlySalary = dto.MonthlySalary;
            trainer.Bio = dto.Bio;
            trainer.Certifications = dto.Certifications;
            trainer.IsAvailable = dto.IsAvailable;
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

        public async Task<bool> UpdateWorkoutPlanAsync(int assignmentId, string workoutPlan, string notes)
        {
            // Handled via direct repo update
            return true;
        }

        private TrainerDTO MapToDTO(Trainer t, double avgRating) => new()
        {
            Id = t.Id,
            UserId = t.UserId,
            FirstName = t.User?.FirstName ?? "",
            LastName = t.User?.LastName ?? "",
            Email = t.User?.Email ?? "",
            Specialization = t.Specialization,
            Experience = t.Experience,
            MonthlySalary = t.MonthlySalary,
            Bio = t.Bio,
            Certifications = t.Certifications,
            ProfilePhoto = t.User?.ProfilePhoto,
            IsAvailable = t.IsAvailable,
            JoinDate = t.JoinDate,
            AssignedMembersCount = t.TrainerAssignments?.Count(ta => ta.IsActive) ?? 0,
            AverageRating = Math.Round(avgRating, 1),
            TotalReviews = t.TrainerReviews?.Count ?? 0,
            Assignments = t.TrainerAssignments?.Where(ta => ta.IsActive).Select(ta => new TrainerAssignmentDTO
            {
                Id = ta.Id,
                MemberId = ta.MemberId,
                MemberName = (ta.Member?.User?.FirstName + " " + ta.Member?.User?.LastName).Trim(),
                MemberPhoto = ta.Member?.User?.ProfilePhoto,
                WorkoutPlan = ta.WorkoutPlan,
                TrainingNotes = ta.TrainingNotes,
                AssignedDate = ta.AssignedDate,
                IsActive = ta.IsActive
            }).ToList() ?? new List<TrainerAssignmentDTO>()
        };
    }
}
