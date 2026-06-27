using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrainerPaymentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "TrainerPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "TrainerPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.UpdateData(
                table: "TrainerPayments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AmountPaid", "PaymentMethod" },
                values: new object[] { 0m, "Cash" });

            migrationBuilder.UpdateData(
                table: "TrainerPayments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AmountPaid", "PaymentMethod" },
                values: new object[] { 0m, "Cash" });

            migrationBuilder.UpdateData(
                table: "TrainerPayments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AmountPaid", "PaymentMethod" },
                values: new object[] { 0m, "Cash" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "TrainerPayments");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "TrainerPayments");

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
        }
    }
}
