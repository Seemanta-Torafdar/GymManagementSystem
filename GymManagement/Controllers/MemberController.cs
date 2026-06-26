using BLL.Interfaces;
using DAL.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Authorize(Roles = "Member")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMembershipService _membershipService;
        private readonly INotificationService _notificationService;
        private readonly IAdminService _adminService;
        private readonly ITrainerService _trainerService;
        private readonly UserManager<User> _userManager;

        public MemberController(IMemberService memberService, IMembershipService membershipService,
            INotificationService notificationService, IAdminService adminService,
            ITrainerService trainerService, UserManager<User> userManager)
        {
            _memberService = memberService; _membershipService = membershipService;
            _notificationService = notificationService; _adminService = adminService;
            _trainerService = trainerService; _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var member = await _memberService.GetByUserIdAsync(user.Id);
            if (member == null) return RedirectToAction("Login", "Account");
            ViewBag.Notifications = await _notificationService.GetByUserIdAsync(user.Id);
            ViewBag.UnreadCount = await _notificationService.GetUnreadCountAsync(user.Id);
            return View(member);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var member = await _memberService.GetByUserIdAsync(user.Id);
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(BLL.DTOs.MemberEditDTO dto)
        {
            await _memberService.UpdateAsync(dto);
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
            await _memberService.UpdateProfilePhotoAsync(user.Id, $"/uploads/profiles/{fileName}");
            TempData["Success"] = "Profile photo updated!";
            return RedirectToAction(nameof(Profile));
        }

        public async Task<IActionResult> Schedule()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var member = await _memberService.GetByUserIdAsync(user.Id);
            ViewBag.Shifts = await _membershipService.GetAllShiftsAsync();
            ViewBag.YogaSchedules = await _membershipService.GetAllYogaAsync();
            ViewBag.CardioSchedules = await _membershipService.GetAllCardioAsync();
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSchedule(int shiftId, int? yogaScheduleId, int? cardioScheduleId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var member = await _memberService.GetByUserIdAsync(user.Id);
            if (member == null) return RedirectToAction(nameof(Dashboard));

            // Get current active purchase to keep existing package
            var activePurchase = await _membershipService.GetActivePurchaseByMemberIdAsync(member.Id);
            if (activePurchase == null)
            {
                TempData["Error"] = "No active membership found. Please contact admin.";
                return RedirectToAction(nameof(Schedule));
            }

            var success = await _membershipService.PurchaseMembershipAsync(
                member.Id, activePurchase.PackageId, shiftId, yogaScheduleId, cardioScheduleId);

            if (success)
                TempData["Success"] = "Schedule updated successfully!";
            else
                TempData["Error"] = "Failed to update schedule. Please try again.";

            return RedirectToAction(nameof(Schedule));
        }

        public async Task<IActionResult> Notifications()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var notifications = await _notificationService.GetByUserIdAsync(user.Id);
            await _notificationService.MarkAllAsReadAsync(user.Id);
            return View(notifications);
        }

        public async Task<IActionResult> MyTrainer()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var member = await _memberService.GetByUserIdAsync(user.Id);
            if (member == null) return RedirectToAction(nameof(Dashboard));
            return View(member);
        }

        public async Task<IActionResult> TrainerProfile(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            var reviews = await _adminService.GetReviewsByTrainerIdAsync(id);
            var user = await _userManager.GetUserAsync(User);
            var member = user != null ? await _memberService.GetByUserIdAsync(user.Id) : null;
            ViewBag.Reviews = reviews;
            ViewBag.MemberId = member?.Id;
            ViewBag.HasReviewed = member != null && await _adminService.HasMemberReviewedTrainerAsync(member.Id, id);
            return View(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> RateTrainer(int trainerId, int rating, string? comment)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var member = await _memberService.GetByUserIdAsync(user.Id);
            if (member == null) return RedirectToAction(nameof(Dashboard));
            await _adminService.AddReviewAsync(member.Id, trainerId, rating, comment);
            TempData["Success"] = "Review submitted successfully!";
            return RedirectToAction(nameof(TrainerProfile), new { id = trainerId });
        }

        public async Task<IActionResult> PurchaseMembership()
        {
            ViewBag.Packages = await _membershipService.GetAllPackagesAsync();
            ViewBag.Shifts = await _membershipService.GetAllShiftsAsync();
            ViewBag.YogaSchedules = await _membershipService.GetAllYogaAsync();
            ViewBag.CardioSchedules = await _membershipService.GetAllCardioAsync();
            return View();
        }
    }
}
