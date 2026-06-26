using BLL.DTOs;
using BLL.Interfaces;
using DAL.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMemberService _memberService;
        private readonly ITrainerService _trainerService;
        private readonly IMembershipService _membershipService;
        private readonly IPaymentService _paymentService;
        private readonly IEquipmentService _equipmentService;
        private readonly INotificationService _notificationService;
        private readonly UserManager<User> _userManager;

        public AdminController(IAdminService adminService, IMemberService memberService, ITrainerService trainerService,
            IMembershipService membershipService, IPaymentService paymentService, IEquipmentService equipmentService,
            INotificationService notificationService, UserManager<User> userManager)
        {
            _adminService = adminService; _memberService = memberService; _trainerService = trainerService;
            _membershipService = membershipService; _paymentService = paymentService;
            _equipmentService = equipmentService; _notificationService = notificationService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var data = await _adminService.GetDashboardDataAsync();
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
                ViewBag.UnreadNotifications = await _notificationService.GetUnreadCountAsync(user.Id);
            return View(data);
        }

        // Members
        public async Task<IActionResult> Members(string? search)
        {
            IEnumerable<MemberDTO> members;
            if (!string.IsNullOrEmpty(search))
                members = await _memberService.SearchAsync(search);
            else
                members = await _memberService.GetAllAsync();
            ViewBag.Search = search;
            return View(members);
        }

        public async Task<IActionResult> MemberDetails(int id)
        {
            var member = await _memberService.GetByIdAsync(id);
            if (member == null) return NotFound();
            return View(member);
        }

        [HttpGet]
        public async Task<IActionResult> CreateMember()
        {
            ViewBag.Packages = await _membershipService.GetAllPackagesAsync();
            ViewBag.Shifts = await _membershipService.GetAllShiftsAsync();
            ViewBag.YogaSchedules = await _membershipService.GetAllYogaAsync();
            ViewBag.CardioSchedules = await _membershipService.GetAllCardioAsync();
            ViewBag.Trainers = await _trainerService.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMember(MemberCreateDTO dto)
        {
            if (!ModelState.IsValid) { return RedirectToAction(nameof(CreateMember)); }
            var result = await _memberService.CreateAsync(dto);
            if (result.Success) { TempData["Success"] = "Member created successfully!"; return RedirectToAction(nameof(Members)); }
            TempData["Error"] = "Failed to create member: " + string.Join(" ", result.Errors);
            return RedirectToAction(nameof(CreateMember));
        }

        [HttpGet]
        public async Task<IActionResult> EditMember(int id)
        {
            var member = await _memberService.GetByIdAsync(id);
            if (member == null) return NotFound();
            var dto = new MemberEditDTO { Id = member.Id, FirstName = member.FirstName, LastName = member.LastName, Phone = member.Phone, Gender = member.Gender, DateOfBirth = member.DateOfBirth, Address = member.Address, BloodGroup = member.BloodGroup, EmergencyContact = member.EmergencyContact, EmergencyPhone = member.EmergencyPhone, MedicalNotes = member.MedicalNotes };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMember(MemberEditDTO dto)
        {
            await _memberService.UpdateAsync(dto);
            TempData["Success"] = "Member updated successfully!";
            return RedirectToAction(nameof(Members));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            {
                await _memberService.DeleteAsync(id);
                TempData["Success"] = "Member deleted.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Cannot delete member. They have existing records (purchases, payments, or trainer assignments).";
            }
            return RedirectToAction(nameof(Members));
        }

        // Trainers
        public async Task<IActionResult> Trainers()
        {
            var trainers = await _trainerService.GetAllAsync();
            return View(trainers);
        }

        public async Task<IActionResult> TrainerDetails(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            var reviews = await _adminService.GetReviewsByTrainerIdAsync(id);
            ViewBag.Reviews = reviews;
            return View(trainer);
        }

        [HttpGet]
        public IActionResult CreateTrainer() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrainer(TrainerCreateDTO dto, IFormFile? profilePhoto)
        {
            if (!ModelState.IsValid) { return View(dto); }
            if (profilePhoto != null) dto.ProfilePhoto = await SaveFileAsync(profilePhoto, "profiles");
            var success = await _trainerService.CreateAsync(dto);
            if (success) { TempData["Success"] = "Trainer created successfully!"; return RedirectToAction(nameof(Trainers)); }
            ModelState.AddModelError("", "Failed to create trainer. The email may already be in use or the password does not meet requirements (min 6 characters, uppercase, digit, special char).");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> EditTrainer(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            return View(new TrainerEditDTO { Id = trainer.Id, Specialization = trainer.Specialization, Experience = trainer.Experience, MonthlySalary = trainer.MonthlySalary, Bio = trainer.Bio, Certifications = trainer.Certifications, IsAvailable = trainer.IsAvailable });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTrainer(TrainerEditDTO dto, IFormFile? profilePhoto)
        {
            if (profilePhoto != null) dto.ProfilePhoto = await SaveFileAsync(profilePhoto, "profiles");
            await _trainerService.UpdateAsync(dto);
            TempData["Success"] = "Trainer updated successfully!";
            return RedirectToAction(nameof(Trainers));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            await _trainerService.DeleteAsync(id);
            TempData["Success"] = "Trainer deleted.";
            return RedirectToAction(nameof(Trainers));
        }

        public async Task<IActionResult> TrainerAllocation(string? search)
        {
            var members = await _memberService.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                members = members.Where(m =>
                    m.FullName.ToLower().Contains(search) ||
                    (m.Email?.ToLower().Contains(search) ?? false));
            }
            var trainers = await _trainerService.GetAllAsync();
            ViewBag.Trainers = trainers;
            ViewBag.Search = search;
            return View(members);
        }

        public async Task<IActionResult> PTMembers(string? search)
        {
            var members = await _memberService.GetAllAsync();
            members = members.Where(m => m.AssignedTrainerId.HasValue); // Only those with allocated trainers
            
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                members = members.Where(m =>
                    m.FullName.ToLower().Contains(search) ||
                    (m.GymId?.ToLower().Contains(search) ?? false) ||
                    (m.Email?.ToLower().Contains(search) ?? false));
            }
            ViewBag.Search = search;
            return View(members);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTrainer(int memberId)
        {
            await _trainerService.RemoveTrainerAssignmentAsync(memberId);
            TempData["Success"] = "Trainer allocation removed.";
            return RedirectToAction(nameof(PTMembers));
        }

        [HttpGet]
        public async Task<IActionResult> AssignTrainer(int memberId)
        {
            var member = await _memberService.GetByIdAsync(memberId);
            var trainers = await _trainerService.GetAllAsync();
            ViewBag.Member = member;
            ViewBag.Trainers = trainers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignTrainer(int memberId, int trainerId, string? workoutPlan, string? notes)
        {
            await _trainerService.AssignMemberAsync(trainerId, memberId, workoutPlan, notes);
            
            // Generate the first month's payment for the training charge
            var trainer = await _trainerService.GetByIdAsync(trainerId);
            if (trainer != null && trainer.TrainingCharge > 0)
            {
                string paymentNotes = $"Monthly Personal Training Fee - {trainer.FullName}";
                await _paymentService.CreatePaymentAsync(memberId, trainer.TrainingCharge, DateTime.Today.AddDays(30), null, paymentNotes);
            }

            TempData["Success"] = "Trainer assigned successfully!";
            return RedirectToAction(nameof(MemberDetails), new { id = memberId });
        }

        // Schedules
        public async Task<IActionResult> Schedules()
        {
            ViewBag.Shifts = await _membershipService.GetAllShiftsAsync();
            ViewBag.YogaSchedules = await _membershipService.GetAllYogaAsync();
            ViewBag.CardioSchedules = await _membershipService.GetAllCardioAsync();
            return View();
        }

        // Settings
        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var settings = await _adminService.GetGymSettingsAsync();
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(GymSettingDTO dto, IFormFile? logoFile, IFormFile? banner1, IFormFile? banner2, IFormFile? banner3)
        {
            if (logoFile != null) dto.LogoPath = await SaveFileAsync(logoFile, "logos");
            if (banner1 != null) dto.BannerImage1 = await SaveFileAsync(banner1, "banners");
            if (banner2 != null) dto.BannerImage2 = await SaveFileAsync(banner2, "banners");
            if (banner3 != null) dto.BannerImage3 = await SaveFileAsync(banner3, "banners");
            await _adminService.UpdateGymSettingsAsync(dto);
            TempData["Success"] = "Settings updated successfully!";
            return RedirectToAction(nameof(Settings));
        }

        // Memberships
        public async Task<IActionResult> Memberships()
        {
            var packages = await _membershipService.GetAllPackagesAsync();
            return View(packages);
        }

        [HttpGet]
        public IActionResult CreatePackage() => View();
        [HttpPost]
        public async Task<IActionResult> CreatePackage(MembershipPackageDTO dto)
        {
            await _membershipService.CreatePackageAsync(dto);
            TempData["Success"] = "Package created!";
            return RedirectToAction(nameof(Memberships));
        }

        [HttpGet]
        public async Task<IActionResult> EditPackage(int id)
        {
            var pkg = await _membershipService.GetPackageByIdAsync(id);
            return pkg == null ? NotFound() : View(pkg);
        }
        [HttpPost]
        public async Task<IActionResult> EditPackage(MembershipPackageDTO dto)
        {
            await _membershipService.UpdatePackageAsync(dto);
            TempData["Success"] = "Package updated!";
            return RedirectToAction(nameof(Memberships));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _membershipService.DeletePackageAsync(id);
            TempData["Success"] = "Package deleted.";
            return RedirectToAction(nameof(Memberships));
        }

        // Renew Membership
        [HttpGet]
        public async Task<IActionResult> RenewMembership(int memberId)
        {
            var member = await _memberService.GetByIdAsync(memberId);
            ViewBag.Member = member;
            ViewBag.Packages = await _membershipService.GetAllPackagesAsync();
            ViewBag.Shifts = await _membershipService.GetAllShiftsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RenewMembership(int memberId, int packageId, int shiftId)
        {
            await _membershipService.RenewMembershipAsync(memberId, packageId, shiftId);
            TempData["Success"] = "Membership renewed!";
            return RedirectToAction(nameof(MemberDetails), new { id = memberId });
        }

        // Equipment
        public async Task<IActionResult> Equipment()
        {
            var equipment = await _equipmentService.GetAllAsync();
            return View(equipment);
        }

        [HttpGet]
        public IActionResult CreateEquipment() => View();

        [HttpPost]
        public async Task<IActionResult> CreateEquipment(EquipmentDTO dto, IFormFile? imageFile)
        {
            string? imagePath = imageFile != null ? await SaveFileAsync(imageFile, "equipment") : null;
            await _equipmentService.CreateAsync(dto, imagePath);
            TempData["Success"] = "Equipment added!";
            return RedirectToAction(nameof(Equipment));
        }

        [HttpGet]
        public async Task<IActionResult> EditEquipment(int id)
        {
            var eq = await _equipmentService.GetByIdAsync(id);
            return eq == null ? NotFound() : View(eq);
        }

        [HttpPost]
        public async Task<IActionResult> EditEquipment(EquipmentDTO dto, IFormFile? imageFile)
        {
            string? imagePath = imageFile != null ? await SaveFileAsync(imageFile, "equipment") : null;
            await _equipmentService.UpdateAsync(dto, imagePath);
            TempData["Success"] = "Equipment updated!";
            return RedirectToAction(nameof(Equipment));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            await _equipmentService.DeleteAsync(id);
            TempData["Success"] = "Equipment deleted.";
            return RedirectToAction(nameof(Equipment));
        }

        // Payments
        public async Task<IActionResult> Payments()
        {
            var payments = await _paymentService.GetAllAsync();
            ViewBag.MonthlyRevenue = await _paymentService.GetMonthlyRevenueAsync(DateTime.Now.Month, DateTime.Now.Year);
            return View(payments);
        }

        [HttpPost]
        public async Task<IActionResult> MarkPaid(int id)
        {
            await _paymentService.MarkAsPaidAsync(id);
            TempData["Success"] = "Payment marked as paid.";
            return RedirectToAction(nameof(Payments));
        }

        [HttpPost]
        public async Task<IActionResult> MarkUnpaid(int id)
        {
            await _paymentService.MarkAsUnpaidAsync(id);
            TempData["Success"] = "Payment marked as unpaid.";
            return RedirectToAction(nameof(Payments));
        }

        public async Task<IActionResult> TrainerPayments()
        {
            var payments = await _paymentService.GetAllTrainerPaymentsAsync();
            ViewBag.Trainers = await _trainerService.GetAllAsync();
            return View(payments);
        }

        [HttpPost]
        public async Task<IActionResult> MarkTrainerPaid(int id)
        {
            await _paymentService.MarkTrainerPaidAsync(id);
            TempData["Success"] = "Trainer payment marked as paid.";
            return RedirectToAction(nameof(TrainerPayments));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainerPayment(int trainerId, int month, int year, decimal amount)
        {
            await _paymentService.CreateTrainerPaymentAsync(trainerId, month, year, amount);
            TempData["Success"] = "Trainer payment record created.";
            return RedirectToAction(nameof(TrainerPayments));
        }

        // --- Gym Shift CRUD ---
        public IActionResult CreateGymShift() => View(new BLL.DTOs.GymShiftDTO { StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(12, 0, 0) });
        [HttpPost]
        public async Task<IActionResult> CreateGymShift(BLL.DTOs.GymShiftDTO dto)
        {
            if (ModelState.IsValid && await _membershipService.CreateShiftAsync(dto))
            {
                TempData["Success"] = "Gym Shift created.";
                return RedirectToAction(nameof(Schedules));
            }
            return View(dto);
        }

        public async Task<IActionResult> EditGymShift(int id)
        {
            var shifts = await _membershipService.GetAllShiftsAsync();
            var shift = shifts.FirstOrDefault(s => s.Id == id);
            return shift == null ? NotFound() : View(shift);
        }
        [HttpPost]
        public async Task<IActionResult> EditGymShift(BLL.DTOs.GymShiftDTO dto)
        {
            if (ModelState.IsValid && await _membershipService.UpdateShiftAsync(dto))
            {
                TempData["Success"] = "Gym Shift updated.";
                return RedirectToAction(nameof(Schedules));
            }
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGymShift(int id)
        {
            await _membershipService.DeleteShiftAsync(id);
            TempData["Success"] = "Gym Shift deleted.";
            return RedirectToAction(nameof(Schedules));
        }

        // --- Yoga CRUD ---
        public IActionResult CreateYogaClass() => View(new BLL.DTOs.YogaScheduleDTO { StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(9, 0, 0) });
        [HttpPost]
        public async Task<IActionResult> CreateYogaClass(BLL.DTOs.YogaScheduleDTO dto)
        {
            if (ModelState.IsValid && await _membershipService.CreateYogaAsync(dto))
            {
                TempData["Success"] = "Yoga Class created.";
                return RedirectToAction(nameof(Schedules));
            }
            return View(dto);
        }
        public async Task<IActionResult> EditYogaClass(int id)
        {
            var classes = await _membershipService.GetAllYogaAsync();
            var yoga = classes.FirstOrDefault(s => s.Id == id);
            return yoga == null ? NotFound() : View(yoga);
        }
        [HttpPost]
        public async Task<IActionResult> EditYogaClass(BLL.DTOs.YogaScheduleDTO dto)
        {
            if (ModelState.IsValid && await _membershipService.UpdateYogaAsync(dto))
            {
                TempData["Success"] = "Yoga Class updated.";
                return RedirectToAction(nameof(Schedules));
            }
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteYogaClass(int id)
        {
            await _membershipService.DeleteYogaAsync(id);
            TempData["Success"] = "Yoga Class deleted.";
            return RedirectToAction(nameof(Schedules));
        }

        // --- Cardio CRUD ---
        public IActionResult CreateCardioClass() => View(new BLL.DTOs.CardioScheduleDTO { StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(18, 0, 0) });
        [HttpPost]
        public async Task<IActionResult> CreateCardioClass(BLL.DTOs.CardioScheduleDTO dto)
        {
            if (ModelState.IsValid && await _membershipService.CreateCardioAsync(dto))
            {
                TempData["Success"] = "Cardio Class created.";
                return RedirectToAction(nameof(Schedules));
            }
            return View(dto);
        }
        public async Task<IActionResult> EditCardioClass(int id)
        {
            var classes = await _membershipService.GetAllCardioAsync();
            var cardio = classes.FirstOrDefault(s => s.Id == id);
            return cardio == null ? NotFound() : View(cardio);
        }
        [HttpPost]
        public async Task<IActionResult> EditCardioClass(BLL.DTOs.CardioScheduleDTO dto)
        {
            if (ModelState.IsValid && await _membershipService.UpdateCardioAsync(dto))
            {
                TempData["Success"] = "Cardio Class updated.";
                return RedirectToAction(nameof(Schedules));
            }
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCardioClass(int id)
        {
            await _membershipService.DeleteCardioAsync(id);
            TempData["Success"] = "Cardio Class deleted.";
            return RedirectToAction(nameof(Schedules));
        }

        private async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", folder);
            Directory.CreateDirectory(uploadsPath);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/uploads/{folder}/{fileName}";
        }
    }
}
