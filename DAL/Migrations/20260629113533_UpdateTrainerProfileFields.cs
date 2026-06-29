using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrainerProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrainingTime",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "Phone", "TrainingTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null });

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateOfBirth", "Phone", "TrainingTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null });

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateOfBirth", "Phone", "TrainingTime" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "TrainingTime",
                table: "Trainers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin",
                column: "ConcurrencyStamp",
                value: "ea341d03-57f2-4e16-9099-ee16df7c3c2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-member",
                column: "ConcurrencyStamp",
                value: "9707be25-16e5-45bc-8b7b-da646992b1fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-trainer",
                column: "ConcurrencyStamp",
                value: "432f92be-2159-40e4-80c5-10951ef261c8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2419079-3909-49e9-b342-6cdf9f9dba62", "AQAAAAIAAYagAAAAEKJT38A4nikgHVJCg2l12zWdnceRnrXuUi5uMhqkuu8kjEIk+2ObF+E9ef6uy3EfSQ==", "4f469c64-1f8f-42cd-b9a5-77e416ed4494" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a95bc90-5096-45f1-8ce0-b5425c60d9b7", "AQAAAAIAAYagAAAAELELyOY+k65+XbPgtmJfpNUdSajF3DahX408aIbTrHu6ENRklKLRAUczQ8kUpAfjOw==", "caa7c004-899d-43b1-b15f-52788508869f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6970cc4c-eed3-4157-be1a-a17048d35517", "AQAAAAIAAYagAAAAEHVgvQM8WnukgDhmznLFOFvGwV5sXkXyzcS/uzoqmhChsH2+xq8OytrNlJUMTgpvzA==", "299f51e7-f830-4307-8796-b2f9afded0af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b0b120d-0a0e-4c59-ab59-0c8d2bdbebeb", "AQAAAAIAAYagAAAAEEew7eKaVQdqpXODQ/CSnszGtKQ3Wi86YLjkeREGlWnYsZUmI6xvZyj5vseiwSzRKQ==", "baef08ad-b057-4dba-8cc7-42a1d879e575" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc83c206-b844-4e18-930e-1955b4e17ad2", "AQAAAAIAAYagAAAAEM5IEwvwL+7U7VO2hyJ24nWgIt0v96HONGgxXaY6A0gCFP4Fr8VhtcnCTjm47ry9yw==", "96d67028-051a-46c7-a81a-4d44c947fb53" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8773568c-818e-4bca-aea7-f4fc34ce5f0a", "AQAAAAIAAYagAAAAEP4apPb/clbdoA2R4MwIjqoWE9ZSdrL1F83PJqUTF35CpeCo7V2ID8yMof+HZQmykQ==", "a8b644e5-a521-47bb-9dbf-082bf7b2c2c6" });
        }
    }
}
