using DAL.EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class GymDbContext : IdentityDbContext<User>
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<MembershipPackage> MembershipPackages { get; set; }
        public DbSet<MembershipPurchase> MembershipPurchases { get; set; }
        public DbSet<GymShift> GymShifts { get; set; }
        public DbSet<YogaSchedule> YogaSchedules { get; set; }
        public DbSet<CardioSchedule> CardioSchedules { get; set; }
        public DbSet<TrainerAssignment> TrainerAssignments { get; set; }
        public DbSet<TrainerReview> TrainerReviews { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentInventory> EquipmentInventories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TrainerPayment> TrainerPayments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<GymSetting> GymSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Member
            builder.Entity<Member>()
                .HasOne(m => m.User)
                .WithOne(u => u.Member)
                .HasForeignKey<Member>(m => m.UserId);

            // Trainer
            builder.Entity<Trainer>()
                .HasOne(t => t.User)
                .WithOne(u => u.Trainer)
                .HasForeignKey<Trainer>(t => t.UserId);

            // Equipment -> Inventory (1:1)
            builder.Entity<EquipmentInventory>()
                .HasOne(ei => ei.Equipment)
                .WithOne(e => e.Inventory)
                .HasForeignKey<EquipmentInventory>(ei => ei.EquipmentId);

            // TrainerAssignment - avoid cascade delete conflicts
            builder.Entity<TrainerAssignment>()
                .HasOne(ta => ta.Trainer)
                .WithMany(t => t.TrainerAssignments)
                .HasForeignKey(ta => ta.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TrainerAssignment>()
                .HasOne(ta => ta.Member)
                .WithMany(m => m.TrainerAssignments)
                .HasForeignKey(ta => ta.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // TrainerReview
            builder.Entity<TrainerReview>()
                .HasOne(tr => tr.Trainer)
                .WithMany(t => t.TrainerReviews)
                .HasForeignKey(tr => tr.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TrainerReview>()
                .HasOne(tr => tr.Member)
                .WithMany(m => m.TrainerReviews)
                .HasForeignKey(tr => tr.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // MembershipPurchase
            builder.Entity<MembershipPurchase>()
                .HasOne(mp => mp.Member)
                .WithMany(m => m.MembershipPurchases)
                .HasForeignKey(mp => mp.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MembershipPurchase>()
                .HasOne(mp => mp.Package)
                .WithMany(p => p.MembershipPurchases)
                .HasForeignKey(mp => mp.PackageId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision
            builder.Entity<MembershipPackage>()
                .Property(p => p.Price).HasPrecision(18, 2);
            builder.Entity<Trainer>()
                .Property(t => t.MonthlySalary).HasPrecision(18, 2);
            builder.Entity<Payment>()
                .Property(p => p.Amount).HasPrecision(18, 2);
            builder.Entity<TrainerPayment>()
                .Property(tp => tp.Amount).HasPrecision(18, 2);
            builder.Entity<EquipmentInventory>()
                .Property(ei => ei.PurchasePrice).HasPrecision(18, 2);

            // ---- SEED DATA ----
            SeedRoles(builder);
            SeedGymSettings(builder);
            SeedMembershipPackages(builder);
            SeedGymShifts(builder);
            SeedYogaSchedules(builder);
            SeedCardioSchedules(builder);
            SeedEquipment(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "role-admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "role-trainer", Name = "Trainer", NormalizedName = "TRAINER" },
                new IdentityRole { Id = "role-member", Name = "Member", NormalizedName = "MEMBER" }
            );

            var hasher = new PasswordHasher<User>();
            var adminUser = new User
            {
                Id = "user-admin-001",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                FirstName = "System",
                LastName = "Admin",
                Role = "Admin",
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = new DateTime(2024, 1, 1)
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin123");
            builder.Entity<User>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "user-admin-001", RoleId = "role-admin" }
            );

            // Seed Trainer Users
            var trainer1 = new User
            {
                Id = "user-trainer-001",
                UserName = "john.trainer@gym.com",
                NormalizedUserName = "JOHN.TRAINER@GYM.COM",
                Email = "john.trainer@gym.com",
                NormalizedEmail = "JOHN.TRAINER@GYM.COM",
                FirstName = "John",
                LastName = "Carter",
                Role = "Trainer",
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = new DateTime(2024, 1, 15)
            };
            trainer1.PasswordHash = hasher.HashPassword(trainer1, "Trainer@123");

            var trainer2 = new User
            {
                Id = "user-trainer-002",
                UserName = "sara.trainer@gym.com",
                NormalizedUserName = "SARA.TRAINER@GYM.COM",
                Email = "sara.trainer@gym.com",
                NormalizedEmail = "SARA.TRAINER@GYM.COM",
                FirstName = "Sara",
                LastName = "Miles",
                Role = "Trainer",
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = new DateTime(2024, 1, 20)
            };
            trainer2.PasswordHash = hasher.HashPassword(trainer2, "Trainer@123");

            var trainer3 = new User
            {
                Id = "user-trainer-003",
                UserName = "mike.trainer@gym.com",
                NormalizedUserName = "MIKE.TRAINER@GYM.COM",
                Email = "mike.trainer@gym.com",
                NormalizedEmail = "MIKE.TRAINER@GYM.COM",
                FirstName = "Mike",
                LastName = "Johnson",
                Role = "Trainer",
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = new DateTime(2024, 2, 1)
            };
            trainer3.PasswordHash = hasher.HashPassword(trainer3, "Trainer@123");

            builder.Entity<User>().HasData(trainer1, trainer2, trainer3);
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "user-trainer-001", RoleId = "role-trainer" },
                new IdentityUserRole<string> { UserId = "user-trainer-002", RoleId = "role-trainer" },
                new IdentityUserRole<string> { UserId = "user-trainer-003", RoleId = "role-trainer" }
            );

            // Trainer entities
            builder.Entity<Trainer>().HasData(
                new Trainer { Id = 1, UserId = "user-trainer-001", Specialization = "Strength & Conditioning", Experience = 8, MonthlySalary = 45000, Bio = "Expert in powerlifting and muscle building with 8 years of coaching experience.", Certifications = "NSCA-CSCS, ACE-CPT", IsAvailable = true, JoinDate = new DateTime(2024, 1, 15) },
                new Trainer { Id = 2, UserId = "user-trainer-002", Specialization = "Yoga & Flexibility", Experience = 6, MonthlySalary = 38000, Bio = "Certified yoga instructor specializing in Hatha and Vinyasa yoga.", Certifications = "RYT-500, ACE-GFI", IsAvailable = true, JoinDate = new DateTime(2024, 1, 20) },
                new Trainer { Id = 3, UserId = "user-trainer-003", Specialization = "Cardio & Weight Loss", Experience = 5, MonthlySalary = 35000, Bio = "Specialist in HIIT training and nutrition coaching for weight management.", Certifications = "ACSM-CPT, Precision Nutrition L1", IsAvailable = true, JoinDate = new DateTime(2024, 2, 1) }
            );

            // Sample Member Users
            var member1 = new User
            {
                Id = "user-member-001",
                UserName = "alex.member@gym.com",
                NormalizedUserName = "ALEX.MEMBER@GYM.COM",
                Email = "alex.member@gym.com",
                NormalizedEmail = "ALEX.MEMBER@GYM.COM",
                FirstName = "Alex",
                LastName = "Brown",
                Role = "Member",
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = new DateTime(2024, 3, 1)
            };
            member1.PasswordHash = hasher.HashPassword(member1, "Member@123");

            var member2 = new User
            {
                Id = "user-member-002",
                UserName = "emma.member@gym.com",
                NormalizedUserName = "EMMA.MEMBER@GYM.COM",
                Email = "emma.member@gym.com",
                NormalizedEmail = "EMMA.MEMBER@GYM.COM",
                FirstName = "Emma",
                LastName = "Wilson",
                Role = "Member",
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = new DateTime(2024, 3, 10)
            };
            member2.PasswordHash = hasher.HashPassword(member2, "Member@123");

            builder.Entity<User>().HasData(member1, member2);
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "user-member-001", RoleId = "role-member" },
                new IdentityUserRole<string> { UserId = "user-member-002", RoleId = "role-member" }
            );

            builder.Entity<Member>().HasData(
                new Member { Id = 1, UserId = "user-member-001", DateOfBirth = new DateTime(1995, 5, 15), Gender = "Male", Phone = "01712345678", EmergencyContact = "Jane Brown", EmergencyPhone = "01798765432", BloodGroup = "O+", Address = "123 Main St, Dhaka", JoinDate = new DateTime(2024, 3, 1) },
                new Member { Id = 2, UserId = "user-member-002", DateOfBirth = new DateTime(1998, 8, 22), Gender = "Female", Phone = "01856789012", EmergencyContact = "Tom Wilson", EmergencyPhone = "01823456789", BloodGroup = "A+", Address = "45 Park Ave, Dhaka", JoinDate = new DateTime(2024, 3, 10) }
            );

            // Sample Membership Purchases
            builder.Entity<MembershipPurchase>().HasData(
                new MembershipPurchase { Id = 1, MemberId = 1, PackageId = 2, ShiftId = 1, YogaScheduleId = null, CardioScheduleId = null, StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 6, 1), IsActive = true, PaymentStatus = "Paid", PurchaseDate = new DateTime(2024, 3, 1) },
                new MembershipPurchase { Id = 2, MemberId = 2, PackageId = 1, ShiftId = 2, YogaScheduleId = 1, CardioScheduleId = null, StartDate = new DateTime(2024, 3, 10), EndDate = new DateTime(2024, 4, 10), IsActive = true, PaymentStatus = "Paid", PurchaseDate = new DateTime(2024, 3, 10) }
            );

            // Trainer Assignments
            builder.Entity<TrainerAssignment>().HasData(
                new TrainerAssignment { Id = 1, TrainerId = 1, MemberId = 1, AssignedDate = new DateTime(2024, 3, 1), WorkoutPlan = "5-day split: Chest/Back/Legs/Shoulders/Arms", TrainingNotes = "Focus on compound movements", IsActive = true },
                new TrainerAssignment { Id = 2, TrainerId = 2, MemberId = 2, AssignedDate = new DateTime(2024, 3, 10), WorkoutPlan = "3-day full body workout + yoga", TrainingNotes = "Beginner friendly routine", IsActive = true }
            );

            // Payments
            builder.Entity<Payment>().HasData(
                new Payment { Id = 1, MemberId = 1, MembershipPurchaseId = 1, Amount = 3500, Status = "Paid", PaymentMethod = "Cash", PaymentDate = new DateTime(2024, 3, 1), DueDate = new DateTime(2024, 3, 1), CreatedAt = new DateTime(2024, 3, 1) },
                new Payment { Id = 2, MemberId = 2, MembershipPurchaseId = 2, Amount = 1500, Status = "Paid", PaymentMethod = "Cash", PaymentDate = new DateTime(2024, 3, 10), DueDate = new DateTime(2024, 3, 10), CreatedAt = new DateTime(2024, 3, 10) }
            );

            // Trainer Payments
            builder.Entity<TrainerPayment>().HasData(
                new TrainerPayment { Id = 1, TrainerId = 1, Month = 3, Year = 2024, Amount = 45000, Status = "Paid", PaidDate = new DateTime(2024, 4, 1), CreatedAt = new DateTime(2024, 3, 31) },
                new TrainerPayment { Id = 2, TrainerId = 2, Month = 3, Year = 2024, Amount = 38000, Status = "Paid", PaidDate = new DateTime(2024, 4, 1), CreatedAt = new DateTime(2024, 3, 31) },
                new TrainerPayment { Id = 3, TrainerId = 3, Month = 3, Year = 2024, Amount = 35000, Status = "Pending", CreatedAt = new DateTime(2024, 3, 31) }
            );

            // Reviews
            builder.Entity<TrainerReview>().HasData(
                new TrainerReview { Id = 1, TrainerId = 1, MemberId = 1, Rating = 5, Comment = "Excellent trainer! Very motivating and knowledgeable.", ReviewDate = new DateTime(2024, 4, 1), IsApproved = true },
                new TrainerReview { Id = 2, TrainerId = 2, MemberId = 2, Rating = 4, Comment = "Great yoga sessions, very patient instructor.", ReviewDate = new DateTime(2024, 4, 5), IsApproved = true }
            );
        }

        private void SeedGymSettings(ModelBuilder builder)
        {
            builder.Entity<GymSetting>().HasData(new GymSetting
            {
                Id = 1,
                GymName = "PowerFit Gym",
                Phone = "+880 1712-345678",
                Email = "info@powerfitgym.com",
                Address = "123 Fitness Street, Gulshan-1, Dhaka 1212, Bangladesh",
                AboutUs = "PowerFit Gym is a state-of-the-art fitness center dedicated to helping you achieve your health and fitness goals. With world-class equipment, expert trainers, and a motivating environment, we've been transforming lives since 2020.",
                HeroTagline = "Transform Your Body. Transform Your Life.",
                UpdatedAt = new DateTime(2024, 1, 1)
            });
        }

        private void SeedMembershipPackages(ModelBuilder builder)
        {
            builder.Entity<MembershipPackage>().HasData(
                new MembershipPackage { Id = 1, Name = "Monthly Package", DurationDays = 30, Price = 1500, Benefits = "Full gym access;Locker room;Free WiFi;1 fitness assessment", Description = "Perfect for trying out our gym. Includes full access to all equipment and basic amenities.", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
                new MembershipPackage { Id = 2, Name = "3-Month Package", DurationDays = 90, Price = 3500, Benefits = "Full gym access;Locker room;Free WiFi;2 fitness assessments;Diet consultation", Description = "Our most popular package. Save 22% compared to monthly billing.", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
                new MembershipPackage { Id = 3, Name = "6-Month Package", DurationDays = 180, Price = 6000, Benefits = "Full gym access;Locker room;Free WiFi;Quarterly assessments;Diet consultation;1 month PT trial", Description = "Serious results require serious commitment. Great value with added benefits.", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
                new MembershipPackage { Id = 4, Name = "Annual Package", DurationDays = 365, Price = 10000, Benefits = "Full gym access;Locker room;Free WiFi;Monthly assessments;Diet consultation;3 months PT included;Priority booking;Guest passes x5", Description = "The ultimate membership. Best value with all premium benefits included.", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) }
            );
        }

        private void SeedGymShifts(ModelBuilder builder)
        {
            builder.Entity<GymShift>().HasData(
                new GymShift { Id = 1, ShiftName = "Morning Shift", StartTime = new TimeSpan(6, 0, 0), EndTime = new TimeSpan(10, 0, 0), Capacity = 50, Description = "Early morning workout session", IsActive = true },
                new GymShift { Id = 2, ShiftName = "Afternoon Shift", StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(16, 0, 0), Capacity = 40, Description = "Midday workout session", IsActive = true },
                new GymShift { Id = 3, ShiftName = "Evening Shift", StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(21, 0, 0), Capacity = 60, Description = "After-work evening session - most popular", IsActive = true }
            );
        }

        private void SeedYogaSchedules(ModelBuilder builder)
        {
            builder.Entity<YogaSchedule>().HasData(
                new YogaSchedule { Id = 1, ClassName = "Hatha Yoga", DayOfWeek = "Monday, Wednesday, Friday", StartTime = new TimeSpan(7, 0, 0), EndTime = new TimeSpan(8, 0, 0), Instructor = "Sara Miles", Capacity = 20, IsActive = true },
                new YogaSchedule { Id = 2, ClassName = "Vinyasa Flow", DayOfWeek = "Tuesday, Thursday", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(19, 0, 0), Instructor = "Sara Miles", Capacity = 15, IsActive = true },
                new YogaSchedule { Id = 3, ClassName = "Restorative Yoga", DayOfWeek = "Saturday", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 30, 0), Instructor = "Sara Miles", Capacity = 25, IsActive = true }
            );
        }

        private void SeedCardioSchedules(ModelBuilder builder)
        {
            builder.Entity<CardioSchedule>().HasData(
                new CardioSchedule { Id = 1, ClassName = "HIIT Training", DayOfWeek = "Monday, Wednesday, Friday", StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(18, 0, 0), EquipmentUsed = "Treadmill, Battle Ropes, Kettlebells", Instructor = "Mike Johnson", Capacity = 25, IsActive = true },
                new CardioSchedule { Id = 2, ClassName = "Cycling Class", DayOfWeek = "Tuesday, Thursday", StartTime = new TimeSpan(7, 0, 0), EndTime = new TimeSpan(8, 0, 0), EquipmentUsed = "Stationary Bikes", Instructor = "Mike Johnson", Capacity = 20, IsActive = true },
                new CardioSchedule { Id = 3, ClassName = "Aerobics", DayOfWeek = "Monday, Wednesday, Saturday", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0), EquipmentUsed = "Open Floor, Step Platforms", Instructor = "John Carter", Capacity = 30, IsActive = true }
            );
        }

        private void SeedEquipment(ModelBuilder builder)
        {
            builder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Olympic Barbell Set", Description = "Professional 20kg Olympic barbell with weight plates (5kg to 25kg). Essential for compound lifts.", Category = "Free Weights", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
                new Equipment { Id = 2, Name = "Treadmill Pro 5000", Description = "Commercial-grade treadmill with 22km/h max speed, incline 0-15%, heart rate monitor, and touchscreen display.", Category = "Cardio", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
                new Equipment { Id = 3, Name = "Cable Cross Machine", Description = "Full commercial cable crossover machine with adjustable pulleys for cable flyes, rows, and tricep pushdowns.", Category = "Strength Machines", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
                new Equipment { Id = 4, Name = "Dumbbells Set (5-50kg)", Description = "Complete rubber hex dumbbell set with dedicated storage rack. Perfect for isolation exercises.", Category = "Free Weights", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
                new Equipment { Id = 5, Name = "Stationary Bike", Description = "Indoor cycling bike with magnetic resistance, adjustable seat, and performance tracking display.", Category = "Cardio", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) },
                new Equipment { Id = 6, Name = "Smith Machine", Description = "Counter-balanced Smith machine for guided barbell training. Ideal for squats, bench press, and shoulder press.", Category = "Strength Machines", IsActive = true, CreatedAt = new DateTime(2024, 1, 1) }
            );

            builder.Entity<EquipmentInventory>().HasData(
                new EquipmentInventory { Id = 1, EquipmentId = 1, Quantity = 10, StockStatus = "Available", PurchaseDate = new DateTime(2024, 1, 5), PurchasePrice = 25000, Supplier = "SportsPro BD", LastUpdated = new DateTime(2024, 1, 5) },
                new EquipmentInventory { Id = 2, EquipmentId = 2, Quantity = 8, StockStatus = "Available", PurchaseDate = new DateTime(2024, 1, 5), PurchasePrice = 150000, Supplier = "FitTech International", LastUpdated = new DateTime(2024, 1, 5) },
                new EquipmentInventory { Id = 3, EquipmentId = 3, Quantity = 3, StockStatus = "Available", PurchaseDate = new DateTime(2024, 1, 5), PurchasePrice = 120000, Supplier = "FitTech International", LastUpdated = new DateTime(2024, 1, 5) },
                new EquipmentInventory { Id = 4, EquipmentId = 4, Quantity = 2, StockStatus = "Low", PurchaseDate = new DateTime(2024, 1, 5), PurchasePrice = 80000, Supplier = "SportsPro BD", LastUpdated = new DateTime(2024, 1, 5) },
                new EquipmentInventory { Id = 5, EquipmentId = 5, Quantity = 12, StockStatus = "Available", PurchaseDate = new DateTime(2024, 1, 5), PurchasePrice = 45000, Supplier = "FitTech International", LastUpdated = new DateTime(2024, 1, 5) },
                new EquipmentInventory { Id = 6, EquipmentId = 6, Quantity = 4, StockStatus = "Available", PurchaseDate = new DateTime(2024, 1, 5), PurchasePrice = 200000, Supplier = "GymWorld", LastUpdated = new DateTime(2024, 1, 5) }
            );
        }
    }
}
