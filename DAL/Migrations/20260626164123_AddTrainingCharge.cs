using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingCharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TrainingCharge",
                table: "Trainers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin",
                column: "ConcurrencyStamp",
                value: "082c359a-b8e2-40c6-968b-43fd00fb8cd1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-member",
                column: "ConcurrencyStamp",
                value: "02e51d9e-3b42-411c-8a24-01b9d8878ba6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-trainer",
                column: "ConcurrencyStamp",
                value: "d8301ec6-dd27-45c8-91f7-1001d5f57d88");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d515bd0-3dd4-4af8-a524-bf49a0f554f8", "AQAAAAIAAYagAAAAENHxIdSBHJlSqr1yaNHP4wToAcjqFl/maGNdBSakcVe4wUjtyKwrztNWdExi71q7vQ==", "30ebf8d5-0746-42e6-a174-6ab99d45416e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b7afd4e-d554-44d7-94c6-0d44995b7175", "AQAAAAIAAYagAAAAEEVVAWXeaWJXV83n5/QfP6osUnKVRkPPqpVy3S+675zZ7FJJJ4fglNrQiEHdy6/dDQ==", "16d7b5aa-1799-469a-8c8f-415378971c9f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d62b6c8d-65f5-4881-a107-5b1cac4b0359", "AQAAAAIAAYagAAAAEMTDUMoYHHOp7c5MhFo0AZ2CUbUD5LEZrFHae/ktLLb3daihGmmqoDZciIpwZIJ8Cg==", "34e3d543-9ede-4a5d-b0f6-ce45d85a3cb4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b373c515-17b2-489a-a425-e99748986904", "AQAAAAIAAYagAAAAEFpiF2yMK9rXua3fUYrQbX6t49NFtvLmBrfgDbx2shDXzXfwiyTTI2YGnTdu8BZkiw==", "f2583bda-b81e-4213-b194-7a483140fbac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a979e4b5-9116-4536-8e0b-35e289b3b663", "AQAAAAIAAYagAAAAEFTSRUouF/QMvs1RTJfT026XODU04QUvamL1fRvl7tq2TTL5G+uMm2PlENEQfyQXEQ==", "6d7aceeb-f9fe-45ee-befc-053512c8eefc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ba757ec-ef14-47ed-81f8-5414143f3ce6", "AQAAAAIAAYagAAAAEIOyo3pc6GIy8wARlD0B6OW/sG8ANIYSkgLWjnDWMAuhcI9GvYYxHS6+wrlm8T9ygQ==", "ec147390-aef3-44d3-8f01-f46caaa0a9b7" });

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "TrainingCharge",
                value: 3000m);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "TrainingCharge",
                value: 2500m);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 3,
                column: "TrainingCharge",
                value: 2000m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingCharge",
                table: "Trainers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin",
                column: "ConcurrencyStamp",
                value: "4b9dbaf8-594d-498e-8b2f-d102d2117078");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-member",
                column: "ConcurrencyStamp",
                value: "7cd0ea92-cd39-4798-9263-e0fbb7da9695");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-trainer",
                column: "ConcurrencyStamp",
                value: "60830331-0de8-474b-b999-eb201784ee79");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62c31d0a-3106-418c-a4c8-b11a24466987", "AQAAAAIAAYagAAAAEMwHGeQth35yuqQaml1TG/8/JcqDQoHbjkmGCbHdTMenk8YbDna9CPSUF93uFV7/Eg==", "0a1acca4-7bb9-4e5c-8236-4cd1ddd094d7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74cdc436-ca18-4fdf-9c7a-e8cf68e85c05", "AQAAAAIAAYagAAAAELEZ2cAYB9qS4N0VP8Ffbikrgydodbo77RTbkPm8XQduLpTuQzLepXBOVvREbPL19g==", "42197155-fa6e-41be-934a-923a63a73d74" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ee1ebc1-35b6-4a5a-9c1a-0b62f01df8f9", "AQAAAAIAAYagAAAAEDAxxK0Rt82oI+TIb04xN0diSGqsOqbS82ksep5eqoRqvniC3TdLjq9QwdGfXfYxSA==", "f94282be-9b99-4e5b-a409-71e7c1d0223e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f5ab158-310c-48ee-921a-616585f431c5", "AQAAAAIAAYagAAAAEPPrNPUIu0OplznPUoykU66GIZ+4KcrugsqBAVdzj74xwDMQFZjQIHV9+YbEbBG1Kg==", "a94e41d4-65ea-4efb-9f52-e31a578f0a0a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "702dc8fe-a163-4882-81d0-30a1956f4efc", "AQAAAAIAAYagAAAAEFCdaFuM51w9DNzSGWSclQI9TyNDxDrwhTlJaD9Mu4/IWTZ2eSJCzxdIP9mhgSly8w==", "082318a5-3310-4cf0-ab38-a4a05aa86351" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5f8e950-92eb-4091-a80b-3398389f647b", "AQAAAAIAAYagAAAAENmqwilbjGe9ovUkjnNEKoIK1tHdrhwp/hxYU0BdK5DUuLvoPcyz4jQZdq32UyqKyw==", "0a954714-f361-448c-a0f7-daf2fb736ded" });
        }
    }
}
