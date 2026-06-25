using BLL.Interfaces;
using DAL.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;
        private readonly INotificationService _notificationService;
        private readonly IAdminService _adminService;
        private readonly UserManager<User> _userManager;

        public TrainerController(ITrainerService trainerService, INotificationService notificationService,
            IAdminService adminService, UserManager<User> userManager)
        {
            _trainerService = trainerService;
            _notificationService = notificationService;
            _adminService = adminService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var trainer = await _trainerService.GetByUserIdAsync(user.Id);
            if (trainer == null) return RedirectToAction("Login", "Account");
            ViewBag.UnreadCount = await _notificationService.GetUnreadCountAsync(user.Id);
            ViewBag.Reviews = await _adminService.GetReviewsByTrainerIdAsync(trainer.Id);
            return View(trainer);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var trainer = await _trainerService.GetByUserIdAsync(user.Id);
            return View(trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(BLL.DTOs.TrainerEditDTO dto)
        {
            await _trainerService.UpdateAsync(dto);
            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || photo == null) return RedirectToAction(nameof(Profile));
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");
            Directory.CreateDirectory(uploadsPath);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            using var stream = new FileStream(Path.Combine(uploadsPath, fileName), FileMode.Create);
            await photo.CopyToAsync(stream);
            await _trainerService.UpdateProfilePhotoAsync(user.Id, $"/uploads/profiles/{fileName}");
            TempData["Success"] = "Profile photo updated!";
            return RedirectToAction(nameof(Profile));
        }

        public async Task<IActionResult> Students()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var trainer = await _trainerService.GetByUserIdAsync(user.Id);
            return View(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWorkout(int assignmentId, string workoutPlan, string notes)
        {
            await _trainerService.UpdateWorkoutPlanAsync(assignmentId, workoutPlan, notes);
            TempData["Success"] = "Workout plan updated!";
            return RedirectToAction(nameof(Students));
        }

        public async Task<IActionResult> Notifications()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var notifications = await _notificationService.GetByUserIdAsync(user.Id);
            await _notificationService.MarkAllAsReadAsync(user.Id);
            return View(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "New passwords do not match.";
                return RedirectToAction(nameof(Profile));
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                TempData["Success"] = "Password changed successfully!";
            }
            else
            {
                TempData["Error"] = string.Join(" ", result.Errors.Select(e => e.Description));
            }
            return RedirectToAction(nameof(Profile));
        }
    }
}
