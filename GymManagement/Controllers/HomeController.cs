using BLL.Interfaces;
using DAL.EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly ITrainerService _trainerService;
        private readonly IEquipmentService _equipmentService;
        private readonly IAdminService _adminService;
        private readonly UserManager<User> _userManager;

        public HomeController(IMembershipService membershipService, ITrainerService trainerService,
            IEquipmentService equipmentService, IAdminService adminService, UserManager<User> userManager)
        {
            _membershipService = membershipService;
            _trainerService = trainerService;
            _equipmentService = equipmentService;
            _adminService = adminService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Packages = await _membershipService.GetAllPackagesAsync();
            ViewBag.Trainers = await _trainerService.GetAllAsync();
            ViewBag.Equipment = await _equipmentService.GetAllAsync();
            ViewBag.Shifts = await _membershipService.GetAllShiftsAsync();
            ViewBag.YogaSchedules = await _membershipService.GetAllYogaAsync();
            ViewBag.CardioSchedules = await _membershipService.GetAllCardioAsync();
            ViewBag.GymSettings = await _adminService.GetGymSettingsAsync();
            return View();
        }

        public IActionResult Privacy() => View();
        public IActionResult AccessDenied() => View();
    }
}
