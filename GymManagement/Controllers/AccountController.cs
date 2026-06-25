using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF.Models;
using GymManagement.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMemberService _memberService;
        private readonly IMembershipService _membershipService;
        private readonly INotificationService _notificationService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IMemberService memberService, IMembershipService membershipService, INotificationService notificationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _memberService = memberService;
            _membershipService = membershipService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToRoleDashboard();
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.IsActive) { await _signInManager.SignOutAsync(); ModelState.AddModelError("", "Your account is deactivated."); return View(model); }
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
                return RedirectToRoleDashboard();
            }
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.Packages = await _membershipService.GetAllPackagesAsync();
            ViewBag.Shifts = await _membershipService.GetAllShiftsAsync();
            ViewBag.YogaSchedules = await _membershipService.GetAllYogaAsync();
            ViewBag.CardioSchedules = await _membershipService.GetAllCardioAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Packages = await _membershipService.GetAllPackagesAsync();
                ViewBag.Shifts = await _membershipService.GetAllShiftsAsync();
                ViewBag.YogaSchedules = await _membershipService.GetAllYogaAsync();
                ViewBag.CardioSchedules = await _membershipService.GetAllCardioAsync();
                return View(model);
            }
            var dto = new MemberCreateDTO
            {
                FirstName = model.FirstName, LastName = model.LastName, Email = model.Email,
                Password = model.Password, Phone = model.Phone, Gender = model.Gender,
                DateOfBirth = model.DateOfBirth, Address = model.Address, BloodGroup = model.BloodGroup,
                EmergencyContact = model.EmergencyContact, EmergencyPhone = model.EmergencyPhone,
                PackageId = model.PackageId, ShiftId = model.ShiftId,
                YogaScheduleId = model.YogaScheduleId, CardioScheduleId = model.CardioScheduleId
            };
            var result = await _memberService.CreateAsync(dto);
            if (result.Success)
            {
                TempData["RegistrationSuccess"] = "🎉 Registration Successful! Welcome to PowerFit Gym. Please log in to get started.";
                return RedirectToAction("Login", "Account");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            ViewBag.Packages = await _membershipService.GetAllPackagesAsync();
            ViewBag.Shifts = await _membershipService.GetAllShiftsAsync();
            ViewBag.YogaSchedules = await _membershipService.GetAllYogaAsync();
            ViewBag.CardioSchedules = await _membershipService.GetAllCardioAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");
            var member = await _memberService.GetByUserIdAsync(user.Id);
            return View(member);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword() => View();

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded) { TempData["Success"] = "Password changed successfully."; return RedirectToAction("Profile"); }
            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(model);
        }

        [Authorize]
        public IActionResult AccessDenied() => View();

        private IActionResult RedirectToRoleDashboard()
        {
            if (User.IsInRole("Admin")) return RedirectToAction("Dashboard", "Admin");
            if (User.IsInRole("Trainer")) return RedirectToAction("Dashboard", "Trainer");
            return RedirectToAction("Dashboard", "Member");
        }
    }
}
