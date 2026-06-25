using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "62c31d0a-3106-418c-a4c8-b11a24466987", "admin@gmail.com", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEMwHGeQth35yuqQaml1TG/8/JcqDQoHbjkmGCbHdTMenk8YbDna9CPSUF93uFV7/Eg==", "0a1acca4-7bb9-4e5c-8236-4cd1ddd094d7", "admin@gmail.com" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin",
                column: "ConcurrencyStamp",
                value: "afeea2b6-8898-4beb-bd0f-369e02bd432a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-member",
                column: "ConcurrencyStamp",
                value: "4b1ecaed-50f0-4876-9631-6c232f20d602");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-trainer",
                column: "ConcurrencyStamp",
                value: "d7cf5f4e-a9fc-4e4d-8cab-a30ef7e8d9ab");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-001",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "618f1edb-99b1-4d67-a4f0-64164bee6583", "admin@gym.com", "ADMIN@GYM.COM", "ADMIN@GYM.COM", "AQAAAAIAAYagAAAAEAOYkpyufz+xQnSHMBBrDnfIHVzieV6KaqzSoILltrxa2fv23nvdbexP9Wy4kbvARA==", "1b4478c3-9685-4d00-97e3-29cfb85ae9c7", "admin@gym.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d709832e-6bf8-4eac-86b2-7da16eb78b57", "AQAAAAIAAYagAAAAEOHGy4/i53+usd/episgXwa/eQWb8HOcAivjxnbQc07pDZNtISGwd5ZmHQV+G4s1TA==", "d5ae559d-5aab-4359-a205-963a3debd190" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6473249-3d91-4d06-82c3-8a5a08b9e8f1", "AQAAAAIAAYagAAAAENOuy9m/eVdXhmtpYL44cVl+J8tOlmOY1EbCY6pmfHN7qlX0PxRhvp9FfKzXcjFwUA==", "afd1048e-6433-4994-9369-85bc663094fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a82efb5b-eb98-41bb-a368-7ba35c949030", "AQAAAAIAAYagAAAAEOdk5OKmpxWQkIct6Umbzsxy/cXb+ew2JO6aCZOEZytl0/CJ2tTE51J9XofKWaS5fg==", "6f76530a-d110-49e2-930a-945e048129b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50c8f244-e76b-4e92-9baa-180a608f630a", "AQAAAAIAAYagAAAAEBBsbWhfmptBTrIOqA3SKJJ85p5ugmEG1MtJR8xPVvEN0hL4f2ZWvN7eNARV1Hy2mg==", "08544b61-0878-4742-93b7-bef0dd989bcc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2b0c2f7-ac33-467c-8ca3-d1c6400f0246", "AQAAAAIAAYagAAAAEDe5/9+skHVi2aQa6fOcbUIcNyQ7B6BmOSuixjwctCy5llnPtBB83b5ie7QBeb2lIA==", "31233a78-f357-480c-8aa9-0d1ebd11df6a" });
        }
    }
}
