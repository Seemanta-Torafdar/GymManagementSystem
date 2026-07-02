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
        private readonly IMemberService _memberService;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<User> _userManager;

        public TrainerController(ITrainerService trainerService, INotificationService notificationService,
            IAdminService adminService, IMemberService memberService, IPaymentService paymentService, UserManager<User> userManager)
        {
            _trainerService = trainerService;
            _notificationService = notificationService;
            _adminService = adminService;
            _memberService = memberService;
            _paymentService = paymentService;
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
            if (trainer == null) return NotFound();
            
            var dto = new BLL.DTOs.TrainerProfileEditDTO
            {
                Id = trainer.Id, FirstName = trainer.FirstName, LastName = trainer.LastName,
                Email = trainer.Email, Phone = trainer.Phone, Specialization = trainer.Specialization,
                Experience = trainer.Experience, Bio = trainer.Bio, Certifications = trainer.Certifications,
                IsAvailable = trainer.IsAvailable, DateOfBirth = trainer.DateOfBirth, TrainingTime = trainer.TrainingTime,
                ProfilePhoto = trainer.ProfilePhoto
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(BLL.DTOs.TrainerProfileEditDTO dto)
        {
            var success = await _trainerService.UpdateProfileAsync(dto);
            if (success)
            {
                var user = await _userManager.GetUserAsync(User);
                var admins = await _userManager.GetUsersInRoleAsync("Admin");
                foreach (var admin in admins)
                {
                    await _notificationService.SendAsync(admin.Id, "Trainer Profile Updated", $"Trainer {dto.FirstName} {dto.LastName} has updated their profile details.");
                }
                TempData["Success"] = "Profile updated successfully!";
            }
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
        public async Task<IActionResult> UpdateWorkout(int assignmentId, IFormFile? workoutPlanFile, string? existingWorkoutPlan, string notes)
        {
            string workoutPlanPath = existingWorkoutPlan ?? "";
            
            if (workoutPlanFile != null && workoutPlanFile.Length > 0)
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "workouts");
                Directory.CreateDirectory(uploadsPath);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(workoutPlanFile.FileName)}";
                using var stream = new FileStream(Path.Combine(uploadsPath, fileName), FileMode.Create);
                await workoutPlanFile.CopyToAsync(stream);
                workoutPlanPath = $"/uploads/workouts/{fileName}";
            }

            await _trainerService.UpdateWorkoutPlanAsync(assignmentId, workoutPlanPath, notes);
            TempData["Success"] = "Workout plan updated!";
            return RedirectToAction(nameof(Students));
        }

        public async Task<IActionResult> StudentDetails(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var trainer = await _trainerService.GetByUserIdAsync(user.Id);
            if (trainer == null) return RedirectToAction("Login", "Account");

            // Verify this student is assigned to this trainer
            var isAssigned = trainer.Assignments.Any(a => a.MemberId == id);
            if (!isAssigned) return Forbid(); // Prevent viewing details of unassigned members

            var student = await _memberService.GetByIdAsync(id);
            if (student == null) return NotFound();
            
            // Pass the specific assignment info to the view as well
            ViewBag.Assignment = trainer.Assignments.First(a => a.MemberId == id);

            return View(student);
        }

        public async Task<IActionResult> Notifications()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var notifications = await _notificationService.GetByUserIdAsync(user.Id);
            await _notificationService.MarkAllAsReadAsync(user.Id);
            return View(notifications);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "New passwords do not match.";
                return RedirectToAction(nameof(ChangePassword));
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
            return RedirectToAction(nameof(ChangePassword));
        }

        public async Task<IActionResult> StudentPayments(int? month, int? year)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var trainer = await _trainerService.GetByUserIdAsync(user.Id);
            if (trainer == null) return RedirectToAction("Login", "Account");

            // Pass null month/year by default so ALL PT payments are shown (not just current month).
            // Trainer was always seeing an empty list if their students' fees were created in a prior month.
            var filterMonth = month;
            var filterYear = year ?? DateTime.Now.Year;

            var payments = await _paymentService.GetStudentPaymentStatusAsync(trainer.Id, filterMonth, filterYear);
            ViewBag.Trainer = trainer;
            ViewBag.Month = filterMonth;  // may be null = "All Months"
            ViewBag.Year = filterYear;
            return View(payments);
        }

        public async Task<IActionResult> MySalary(int? month, int? year)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var trainer = await _trainerService.GetByUserIdAsync(user.Id);
            if (trainer == null) return RedirectToAction("Login", "Account");

            var filterYear = year ?? DateTime.Now.Year;

            // If month is not provided and it's a direct navigation, we might want to show all months by default, or just current.
            // Let's pass the raw month filter down. If it's null, we filter by null (which means all months).
            var payments = await _paymentService.GetFilteredTrainerPaymentsAsync(null, trainer.Id, month, filterYear);
            
            // Sort by month descending so newest is first
            payments = payments.OrderByDescending(p => p.Month).ToList();

            ViewBag.Month = month; // null means 'All Months'
            ViewBag.Year = filterYear;
            return View(payments);
        }
    }
}
