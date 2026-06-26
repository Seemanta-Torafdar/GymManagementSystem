using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddGymId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GymId",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GymId",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin",
                column: "ConcurrencyStamp",
                value: "3c4cd836-659c-4f32-9c2c-20d743b6c0a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-member",
                column: "ConcurrencyStamp",
                value: "561bcc25-2a51-40db-b13b-9407a96b6cfb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-trainer",
                column: "ConcurrencyStamp",
                value: "af767cdb-8385-47db-9c22-273fa34e10ef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db6b41a8-5348-4ded-99a8-89cf1d5c7208", "AQAAAAIAAYagAAAAEEZAKpnhS+c1Wy8C2H0+xDmEistRwdxG0Fj8+A+Cqu7HJ4rbG5sfoyJpE4nD5JG80w==", "78d32668-03bc-4608-8fb1-8611a03ab838" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7962a3d-37fd-43e8-a3cf-3523f8e89c7b", "AQAAAAIAAYagAAAAEIiSgSwGEZULB8m6GVZTGJHzzMS802PNo3aMXUnXR/qUsKLVf04nRjkLzv85AZMd0g==", "96195657-3029-4ab8-a19c-435205624d6e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af528586-497d-4274-93e9-497e5e9b8885", "AQAAAAIAAYagAAAAEAr6TsqkILveayozDzWck5nkf2uj6BJRivJ1JHUYm/f3iSTpFb/Pma+gMbb4T4hYWg==", "eee3de16-2c27-4312-be56-7653dd3ea327" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfa9c8e2-e2d8-4093-839a-db735cf74e9a", "AQAAAAIAAYagAAAAECzTb1d8PikVcbuDXMRbrcS/6lrxHkFMQmeBuSivFNLN3RB9ycMueia/JMNwqCxzBQ==", "c74cc880-847b-4009-9c76-6934080adaac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "543c7423-bd6a-41d9-9698-9ffc745c390c", "AQAAAAIAAYagAAAAEEzVMIBp6cm4TUnwS2YloSZ9Obhz2y4gjdHDTQJX+1kJ4h84GFhhdawrVs7TjITzvw==", "ff175e18-0cca-4dbd-b0a6-f3fad19dd45a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa330652-ae4d-44a0-9b75-6a8a9ba7d25e", "AQAAAAIAAYagAAAAEMQJnMpAFFYlIk+JuK2JROSKQWsHPJU6ABgEt3s8g4NcxrH1gRcEflm24yL2j4djRw==", "71d2c243-5267-49c4-85c6-585fef461329" });

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1,
                column: "GymId",
                value: "");

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2,
                column: "GymId",
                value: "");

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "GymId",
                value: "");

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "GymId",
                value: "");

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 3,
                column: "GymId",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Members");

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
        }
    }
}
