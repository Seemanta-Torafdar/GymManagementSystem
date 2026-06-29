using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class PaymentModuleOverhaul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TrainerPayments",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "PaidDate",
                table: "TrainerPayments",
                newName: "LastPaidDate");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "TrainerPayments",
                newName: "TotalSalary");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payments",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payments",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<string>(
                name: "AvailableTimeSlots",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxStudentsPerSlot",
                table: "Trainers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalTrainingCharge",
                table: "Trainers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "Payments",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PackageName",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonalTrainingSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChargePerSession = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalTrainingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalTrainingSessions_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalTrainingSessions_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin",
                column: "ConcurrencyStamp",
                value: "f9a94858-7319-4a6f-acb0-b81a4fdccdc7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-member",
                column: "ConcurrencyStamp",
                value: "732ef198-e717-418a-b396-0e5df3ba4dcc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-trainer",
                column: "ConcurrencyStamp",
                value: "6034776f-d4f5-446c-ab6a-f9818c64d089");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13d1921a-dd55-4d59-890b-a5c14dc3ccc1", "AQAAAAIAAYagAAAAEDz5Pi8Ot7XNJS74oloiQhpOEiwfNbgpLMneLGVGylput0N7twH/lju6Q7mWWyKgPg==", "a6fe1100-076d-420d-89ac-dc1aefde9c16" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba17968c-8e78-4826-ae2a-4c297bb9e8b1", "AQAAAAIAAYagAAAAEHMwffiNVbIhaiyKTtrlJ2X6ntzRogRIwLHPAQmkdaN5lV9v4wROLyFA+ScG+TLxCQ==", "9cc79e67-8ba0-4d06-9062-53d9ddce3b04" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ece28d4-fdb0-4ecf-8668-466c861ad949", "AQAAAAIAAYagAAAAELoG8t1LRa3C7EN4pbY9xpl1/bZyxBOnc2V7wXzXWG3rICjpI37JJA5Goius05Vafg==", "022b53b6-db0a-44b1-9fe6-9d49c33fd657" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75921df5-93ab-40ad-9f83-a14c8736a80d", "AQAAAAIAAYagAAAAEF57H5BKt3OtOzXPS4ctlJ9z8roHxU+TaKQOQIycGU142R8DzAJ+qW3zWdqKvFo/Cw==", "b9e07f0e-688a-43a0-8cca-61beea2d3f98" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea6a81c9-131b-4d64-8b4a-0c75c373ee9e", "AQAAAAIAAYagAAAAEIndLXYhxr5eGqFWSaLbXck7eqK+ETgijohxe8RuEAuJOcFChS60cAV0d/miKxiD7w==", "baec1432-22e5-46e7-951f-67246aac12a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8230eca7-8c68-452e-b667-ac6f78b64180", "AQAAAAIAAYagAAAAEEu52TLfoiMySVZQ/OY3R9cf7mR61xPiIKcga2VPCF1we9YLX4w03gMfR8F+O2ee7Q==", "03ebb81d-7593-4aa3-9084-a22f99b49163" });

            migrationBuilder.UpdateData(
                table: "TrainerPayments",
                keyColumn: "Id",
                keyValue: 1,
                column: "AmountPaid",
                value: 45000m);

            migrationBuilder.UpdateData(
                table: "TrainerPayments",
                keyColumn: "Id",
                keyValue: 2,
                column: "AmountPaid",
                value: 38000m);

            migrationBuilder.UpdateData(
                table: "TrainerPayments",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvailableTimeSlots", "MaxStudentsPerSlot", "PersonalTrainingCharge" },
                values: new object[] { null, 2, 0m });

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvailableTimeSlots", "MaxStudentsPerSlot", "PersonalTrainingCharge" },
                values: new object[] { null, 2, 0m });

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvailableTimeSlots", "MaxStudentsPerSlot", "PersonalTrainingCharge" },
                values: new object[] { null, 2, 0m });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTrainingSessions_MemberId",
                table: "PersonalTrainingSessions",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTrainingSessions_TrainerId",
                table: "PersonalTrainingSessions",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalTrainingSessions");

            migrationBuilder.DropColumn(
                name: "AvailableTimeSlots",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "MaxStudentsPerSlot",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "PersonalTrainingCharge",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PackageName",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "TotalSalary",
                table: "TrainerPayments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "TrainerPayments",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "LastPaidDate",
                table: "TrainerPayments",
                newName: "PaidDate");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Payments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Payments",
                newName: "Status");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin",
                column: "ConcurrencyStamp",
                value: "2858438b-8716-473d-9b9c-e0d0d1c6f24b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-member",
                column: "ConcurrencyStamp",
                value: "4ab6034b-8dbc-41c1-8a7a-e792f3f0cf4e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-trainer",
                column: "ConcurrencyStamp",
                value: "01ead314-18ae-45cf-9d13-aa97e638ea54");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39f96955-1a8e-4461-884e-f73de75f4c5c", "AQAAAAIAAYagAAAAEJR+/SiwIASI0JgJnzXUkI7AiDvgXDBunJksz6yswHeuG9GMWZrRVOzrfao3RSAvSQ==", "97531626-e25a-4bd1-ba18-8494bfc3b604" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ac021e8-ea7f-4766-971d-94b723b01288", "AQAAAAIAAYagAAAAEHHQM09TBjqI794pAouo+Esj+tRlzDgwti06zRAshyKvXwC8e7SSXVqGJoXWa6sk9w==", "210810ee-33d3-4d41-b427-5dbc78188c45" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8c47213-450a-4217-94ab-8421e4c6fb09", "AQAAAAIAAYagAAAAEOG6KREIQtITB/Qk2VnOm+oC41AWxOD96ezInQJo3y5wHomP4wncYP+3UBmJvIbNTA==", "83664dd3-54e3-4bc2-8f97-25564131a58f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c28e9167-1a2f-4d97-9c83-bb03f15c4742", "AQAAAAIAAYagAAAAEH2Bcj2vfjsHWmh9fm4BfHMg/Gyc3Cfj+xmQqQEmggtxl5K0gPpLUkDTi0P7aN9Vmg==", "5e96bdae-4314-4d55-af00-54bcd3afcc33" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "103583c0-6922-47c9-9f3d-dd4bc1006404", "AQAAAAIAAYagAAAAEL5ZNvuyoRyvBO9tQQYho5/LaunUDu4WkvuOBBxZPa+WyvoluJ0npswhVTTh0b/dFw==", "f3a9bcb5-6d19-4e77-9ad8-30e99b6b9ce6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a4c18b9-8d68-4512-8755-ccbc6cb1e070", "AQAAAAIAAYagAAAAEHOEseLW8uvfW9/qoe1eueaB6Gs7SYawjThviqY6RWxy1B1r4uva8mogvZdWXxKSsw==", "100fb9e3-5f8f-4aaa-92f5-6a4afd500d40" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "CreatedAt", "DueDate", "MemberId", "MembershipPurchaseId", "Notes", "PaymentDate", "PaymentMethod", "Status" },
                values: new object[,]
                {
                    { 1, 3500m, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cash", "Paid" },
                    { 2, 1500m, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, null, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cash", "Paid" }
                });

            migrationBuilder.UpdateData(
                table: "TrainerPayments",
                keyColumn: "Id",
                keyValue: 1,
                column: "AmountPaid",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "TrainerPayments",
                keyColumn: "Id",
                keyValue: 2,
                column: "AmountPaid",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "TrainerPayments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Status",
                value: "Pending");
        }
    }
}
