using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardioSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EquipmentUsed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instructor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardioSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GymSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutUs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwitterUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YouTubeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannerImage1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannerImage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannerImage3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeroTagline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GymShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymShifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Benefits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YogaSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Instructor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YogaSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    MonthlySalary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    StockStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentInventories_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    YogaScheduleId = table.Column<int>(type: "int", nullable: true),
                    CardioScheduleId = table.Column<int>(type: "int", nullable: true),
                    TrainerAssignmentId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipPurchases_CardioSchedules_CardioScheduleId",
                        column: x => x.CardioScheduleId,
                        principalTable: "CardioSchedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MembershipPurchases_GymShifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "GymShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembershipPurchases_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembershipPurchases_MembershipPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "MembershipPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembershipPurchases_YogaSchedules_YogaScheduleId",
                        column: x => x.YogaScheduleId,
                        principalTable: "YogaSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    MembershipPurchaseId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkoutPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerAssignments_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainerAssignments_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainerPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerPayments_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerReviews_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainerReviews_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin", "afeea2b6-8898-4beb-bd0f-369e02bd432a", "Admin", "ADMIN" },
                    { "role-member", "4b1ecaed-50f0-4876-9631-6c232f20d602", "Member", "MEMBER" },
                    { "role-trainer", "d7cf5f4e-a9fc-4e4d-8cab-a30ef7e8d9ab", "Trainer", "TRAINER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePhoto", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user-admin-001", 0, "618f1edb-99b1-4d67-a4f0-64164bee6583", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gym.com", true, "System", true, "Admin", false, null, "ADMIN@GYM.COM", "ADMIN@GYM.COM", "AQAAAAIAAYagAAAAEAOYkpyufz+xQnSHMBBrDnfIHVzieV6KaqzSoILltrxa2fv23nvdbexP9Wy4kbvARA==", null, false, null, "Admin", "1b4478c3-9685-4d00-97e3-29cfb85ae9c7", false, "admin@gym.com" },
                    { "user-member-001", 0, "d709832e-6bf8-4eac-86b2-7da16eb78b57", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alex.member@gym.com", true, "Alex", true, "Brown", false, null, "ALEX.MEMBER@GYM.COM", "ALEX.MEMBER@GYM.COM", "AQAAAAIAAYagAAAAEOHGy4/i53+usd/episgXwa/eQWb8HOcAivjxnbQc07pDZNtISGwd5ZmHQV+G4s1TA==", null, false, null, "Member", "d5ae559d-5aab-4359-a205-963a3debd190", false, "alex.member@gym.com" },
                    { "user-member-002", 0, "c6473249-3d91-4d06-82c3-8a5a08b9e8f1", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "emma.member@gym.com", true, "Emma", true, "Wilson", false, null, "EMMA.MEMBER@GYM.COM", "EMMA.MEMBER@GYM.COM", "AQAAAAIAAYagAAAAENOuy9m/eVdXhmtpYL44cVl+J8tOlmOY1EbCY6pmfHN7qlX0PxRhvp9FfKzXcjFwUA==", null, false, null, "Member", "afd1048e-6433-4994-9369-85bc663094fc", false, "emma.member@gym.com" },
                    { "user-trainer-001", 0, "a82efb5b-eb98-41bb-a368-7ba35c949030", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.trainer@gym.com", true, "John", true, "Carter", false, null, "JOHN.TRAINER@GYM.COM", "JOHN.TRAINER@GYM.COM", "AQAAAAIAAYagAAAAEOdk5OKmpxWQkIct6Umbzsxy/cXb+ew2JO6aCZOEZytl0/CJ2tTE51J9XofKWaS5fg==", null, false, null, "Trainer", "6f76530a-d110-49e2-930a-945e048129b1", false, "john.trainer@gym.com" },
                    { "user-trainer-002", 0, "50c8f244-e76b-4e92-9baa-180a608f630a", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.trainer@gym.com", true, "Sara", true, "Miles", false, null, "SARA.TRAINER@GYM.COM", "SARA.TRAINER@GYM.COM", "AQAAAAIAAYagAAAAEBBsbWhfmptBTrIOqA3SKJJ85p5ugmEG1MtJR8xPVvEN0hL4f2ZWvN7eNARV1Hy2mg==", null, false, null, "Trainer", "08544b61-0878-4742-93b7-bef0dd989bcc", false, "sara.trainer@gym.com" },
                    { "user-trainer-003", 0, "e2b0c2f7-ac33-467c-8ca3-d1c6400f0246", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mike.trainer@gym.com", true, "Mike", true, "Johnson", false, null, "MIKE.TRAINER@GYM.COM", "MIKE.TRAINER@GYM.COM", "AQAAAAIAAYagAAAAEDe5/9+skHVi2aQa6fOcbUIcNyQ7B6BmOSuixjwctCy5llnPtBB83b5ie7QBeb2lIA==", null, false, null, "Trainer", "31233a78-f357-480c-8aa9-0d1ebd11df6a", false, "mike.trainer@gym.com" }
                });

            migrationBuilder.InsertData(
                table: "CardioSchedules",
                columns: new[] { "Id", "Capacity", "ClassName", "DayOfWeek", "EndTime", "EquipmentUsed", "Instructor", "IsActive", "StartTime" },
                values: new object[,]
                {
                    { 1, 25, "HIIT Training", "Monday, Wednesday, Friday", new TimeSpan(0, 18, 0, 0, 0), "Treadmill, Battle Ropes, Kettlebells", "Mike Johnson", true, new TimeSpan(0, 17, 0, 0, 0) },
                    { 2, 20, "Cycling Class", "Tuesday, Thursday", new TimeSpan(0, 8, 0, 0, 0), "Stationary Bikes", "Mike Johnson", true, new TimeSpan(0, 7, 0, 0, 0) },
                    { 3, 30, "Aerobics", "Monday, Wednesday, Saturday", new TimeSpan(0, 10, 0, 0, 0), "Open Floor, Step Platforms", "John Carter", true, new TimeSpan(0, 9, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImagePath", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Free Weights", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Professional 20kg Olympic barbell with weight plates (5kg to 25kg). Essential for compound lifts.", null, true, "Olympic Barbell Set" },
                    { 2, "Cardio", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Commercial-grade treadmill with 22km/h max speed, incline 0-15%, heart rate monitor, and touchscreen display.", null, true, "Treadmill Pro 5000" },
                    { 3, "Strength Machines", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full commercial cable crossover machine with adjustable pulleys for cable flyes, rows, and tricep pushdowns.", null, true, "Cable Cross Machine" },
                    { 4, "Free Weights", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Complete rubber hex dumbbell set with dedicated storage rack. Perfect for isolation exercises.", null, true, "Dumbbells Set (5-50kg)" },
                    { 5, "Cardio", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indoor cycling bike with magnetic resistance, adjustable seat, and performance tracking display.", null, true, "Stationary Bike" },
                    { 6, "Strength Machines", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Counter-balanced Smith machine for guided barbell training. Ideal for squats, bench press, and shoulder press.", null, true, "Smith Machine" }
                });

            migrationBuilder.InsertData(
                table: "GymSettings",
                columns: new[] { "Id", "AboutUs", "Address", "BannerImage1", "BannerImage2", "BannerImage3", "Email", "FacebookUrl", "GymName", "HeroTagline", "InstagramUrl", "LogoPath", "Phone", "TwitterUrl", "UpdatedAt", "YouTubeUrl" },
                values: new object[] { 1, "PowerFit Gym is a state-of-the-art fitness center dedicated to helping you achieve your health and fitness goals. With world-class equipment, expert trainers, and a motivating environment, we've been transforming lives since 2020.", "123 Fitness Street, Gulshan-1, Dhaka 1212, Bangladesh", null, null, null, "info@powerfitgym.com", null, "PowerFit Gym", "Transform Your Body. Transform Your Life.", null, null, "+880 1712-345678", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "GymShifts",
                columns: new[] { "Id", "Capacity", "Description", "EndTime", "IsActive", "ShiftName", "StartTime" },
                values: new object[,]
                {
                    { 1, 50, "Early morning workout session", new TimeSpan(0, 10, 0, 0, 0), true, "Morning Shift", new TimeSpan(0, 6, 0, 0, 0) },
                    { 2, 40, "Midday workout session", new TimeSpan(0, 16, 0, 0, 0), true, "Afternoon Shift", new TimeSpan(0, 12, 0, 0, 0) },
                    { 3, 60, "After-work evening session - most popular", new TimeSpan(0, 21, 0, 0, 0), true, "Evening Shift", new TimeSpan(0, 17, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "MembershipPackages",
                columns: new[] { "Id", "Benefits", "CreatedAt", "Description", "DurationDays", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Full gym access;Locker room;Free WiFi;1 fitness assessment", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Perfect for trying out our gym. Includes full access to all equipment and basic amenities.", 30, true, "Monthly Package", 1500m },
                    { 2, "Full gym access;Locker room;Free WiFi;2 fitness assessments;Diet consultation", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Our most popular package. Save 22% compared to monthly billing.", 90, true, "3-Month Package", 3500m },
                    { 3, "Full gym access;Locker room;Free WiFi;Quarterly assessments;Diet consultation;1 month PT trial", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Serious results require serious commitment. Great value with added benefits.", 180, true, "6-Month Package", 6000m },
                    { 4, "Full gym access;Locker room;Free WiFi;Monthly assessments;Diet consultation;3 months PT included;Priority booking;Guest passes x5", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The ultimate membership. Best value with all premium benefits included.", 365, true, "Annual Package", 10000m }
                });

            migrationBuilder.InsertData(
                table: "YogaSchedules",
                columns: new[] { "Id", "Capacity", "ClassName", "DayOfWeek", "EndTime", "Instructor", "IsActive", "StartTime" },
                values: new object[,]
                {
                    { 1, 20, "Hatha Yoga", "Monday, Wednesday, Friday", new TimeSpan(0, 8, 0, 0, 0), "Sara Miles", true, new TimeSpan(0, 7, 0, 0, 0) },
                    { 2, 15, "Vinyasa Flow", "Tuesday, Thursday", new TimeSpan(0, 19, 0, 0, 0), "Sara Miles", true, new TimeSpan(0, 18, 0, 0, 0) },
                    { 3, 25, "Restorative Yoga", "Saturday", new TimeSpan(0, 10, 30, 0, 0), "Sara Miles", true, new TimeSpan(0, 9, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "role-admin", "user-admin-001" },
                    { "role-member", "user-member-001" },
                    { "role-member", "user-member-002" },
                    { "role-trainer", "user-trainer-001" },
                    { "role-trainer", "user-trainer-002" },
                    { "role-trainer", "user-trainer-003" }
                });

            migrationBuilder.InsertData(
                table: "EquipmentInventories",
                columns: new[] { "Id", "EquipmentId", "LastUpdated", "Notes", "PurchaseDate", "PurchasePrice", "Quantity", "StockStatus", "Supplier" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 25000m, 10, "Available", "SportsPro BD" },
                    { 2, 2, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 150000m, 8, "Available", "FitTech International" },
                    { 3, 3, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 120000m, 3, "Available", "FitTech International" },
                    { 4, 4, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 80000m, 2, "Low", "SportsPro BD" },
                    { 5, 5, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 45000m, 12, "Available", "FitTech International" },
                    { 6, 6, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 200000m, 4, "Available", "GymWorld" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "BloodGroup", "DateOfBirth", "EmergencyContact", "EmergencyPhone", "Gender", "JoinDate", "MedicalNotes", "Phone", "UserId" },
                values: new object[,]
                {
                    { 1, "123 Main St, Dhaka", "O+", new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Brown", "01798765432", "Male", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "01712345678", "user-member-001" },
                    { 2, "45 Park Ave, Dhaka", "A+", new DateTime(1998, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tom Wilson", "01823456789", "Female", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "01856789012", "user-member-002" }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Bio", "Certifications", "Experience", "IsAvailable", "JoinDate", "MonthlySalary", "Specialization", "UserId" },
                values: new object[,]
                {
                    { 1, "Expert in powerlifting and muscle building with 8 years of coaching experience.", "NSCA-CSCS, ACE-CPT", 8, true, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 45000m, "Strength & Conditioning", "user-trainer-001" },
                    { 2, "Certified yoga instructor specializing in Hatha and Vinyasa yoga.", "RYT-500, ACE-GFI", 6, true, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 38000m, "Yoga & Flexibility", "user-trainer-002" },
                    { 3, "Specialist in HIIT training and nutrition coaching for weight management.", "ACSM-CPT, Precision Nutrition L1", 5, true, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 35000m, "Cardio & Weight Loss", "user-trainer-003" }
                });

            migrationBuilder.InsertData(
                table: "MembershipPurchases",
                columns: new[] { "Id", "CardioScheduleId", "EndDate", "IsActive", "MemberId", "Notes", "PackageId", "PaymentStatus", "PurchaseDate", "ShiftId", "StartDate", "TrainerAssignmentId", "YogaScheduleId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, null, 2, "Paid", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 2, null, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, null, 1, "Paid", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "CreatedAt", "DueDate", "MemberId", "MembershipPurchaseId", "Notes", "PaymentDate", "PaymentMethod", "Status" },
                values: new object[,]
                {
                    { 1, 3500m, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cash", "Paid" },
                    { 2, 1500m, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cash", "Paid" }
                });

            migrationBuilder.InsertData(
                table: "TrainerAssignments",
                columns: new[] { "Id", "AssignedDate", "IsActive", "MemberId", "TrainerId", "TrainingNotes", "WorkoutPlan" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 1, "Focus on compound movements", "5-day split: Chest/Back/Legs/Shoulders/Arms" },
                    { 2, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 2, "Beginner friendly routine", "3-day full body workout + yoga" }
                });

            migrationBuilder.InsertData(
                table: "TrainerPayments",
                columns: new[] { "Id", "Amount", "CreatedAt", "Month", "Notes", "PaidDate", "Status", "TrainerId", "Year" },
                values: new object[,]
                {
                    { 1, 45000m, new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paid", 1, 2024 },
                    { 2, 38000m, new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paid", 2, 2024 },
                    { 3, 35000m, new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, null, "Pending", 3, 2024 }
                });

            migrationBuilder.InsertData(
                table: "TrainerReviews",
                columns: new[] { "Id", "Comment", "IsApproved", "MemberId", "Rating", "ReviewDate", "TrainerId" },
                values: new object[,]
                {
                    { 1, "Excellent trainer! Very motivating and knowledgeable.", true, 1, 5, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Great yoga sessions, very patient instructor.", true, 2, 4, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentInventories_EquipmentId",
                table: "EquipmentInventories",
                column: "EquipmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserId",
                table: "Members",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPurchases_CardioScheduleId",
                table: "MembershipPurchases",
                column: "CardioScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPurchases_MemberId",
                table: "MembershipPurchases",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPurchases_PackageId",
                table: "MembershipPurchases",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPurchases_ShiftId",
                table: "MembershipPurchases",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPurchases_YogaScheduleId",
                table: "MembershipPurchases",
                column: "YogaScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MemberId",
                table: "Payments",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerAssignments_MemberId",
                table: "TrainerAssignments",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerAssignments_TrainerId",
                table: "TrainerAssignments",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerPayments_TrainerId",
                table: "TrainerPayments",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerReviews_MemberId",
                table: "TrainerReviews",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerReviews_TrainerId",
                table: "TrainerReviews",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_UserId",
                table: "Trainers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EquipmentInventories");

            migrationBuilder.DropTable(
                name: "GymSettings");

            migrationBuilder.DropTable(
                name: "MembershipPurchases");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "TrainerAssignments");

            migrationBuilder.DropTable(
                name: "TrainerPayments");

            migrationBuilder.DropTable(
                name: "TrainerReviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "CardioSchedules");

            migrationBuilder.DropTable(
                name: "GymShifts");

            migrationBuilder.DropTable(
                name: "MembershipPackages");

            migrationBuilder.DropTable(
                name: "YogaSchedules");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
