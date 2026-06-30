using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonalTrainingCharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PersonalTrainingCharge",
                table: "TrainerAssignments",
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
                value: "1c9dd1c2-2df0-4961-a769-fd4eab8995d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-member",
                column: "ConcurrencyStamp",
                value: "da19bf7f-04f7-4dae-88f0-61b7658d2b84");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-trainer",
                column: "ConcurrencyStamp",
                value: "2036195c-ab79-48a6-a6f8-c00ea196a9c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10a6d45c-8817-4ec9-80a7-e85cd91e7b31", "AQAAAAIAAYagAAAAEADYhlNhi77w6VCSbMv573UgltJy+2FTs2w4mh20ym82zPJRjt9+DmlUqQMo7Y+Q8w==", "880ef097-e9e9-4943-a3da-049ff286e1d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0a81c40-cd05-423f-8e4d-67de24332b16", "AQAAAAIAAYagAAAAEC1A7zCiQYkJBtC4jJ9KAFo212bOccEc2RuXnA9e3fbBa5641jzGpMz54GO929L7hA==", "686f0350-5832-4ab7-8ee8-12394acc778c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-member-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c57c7052-7a9a-4612-9763-f13b473a9b37", "AQAAAAIAAYagAAAAEMiP4GN11js0qQ25Ex8Cm1392vKIBhr4Ei5PG2+/pSn5dNQNd6xx4SrGYJy3eMqCZg==", "1f6ce219-a929-4e2d-b38b-90a605b10e70" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13e555f9-006d-4f95-84a3-d951d29dfdcb", "AQAAAAIAAYagAAAAEL451WBYL+rWhfbhdkYtTesmF/KhxI5mrKH+3S66de+AhHE7NruXBB6W8Ivj98gtFQ==", "11035ca3-9dda-4141-9347-d5ab52e2d140" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9d45015-829d-48fe-81b7-78295d7ad65d", "AQAAAAIAAYagAAAAEE+xQx4M0Y8839U+yUMVsbGARvC6AoDFJal+TY5TLpHT/ahdAqkyu0EshB5htq/uvw==", "294499a9-9ab2-47cc-90a1-223fb96a8598" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-trainer-003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49e4e503-f4b4-477b-80b0-699ab8689bd5", "AQAAAAIAAYagAAAAEKxkBn9HLWlUQK3Vv6SXYztd4nwi9lnBVl6cvXrLETsYcf3l1Ox5hangJ4Xm3/CtJA==", "b539c513-bacc-47ae-8411-1748df03292e" });

            migrationBuilder.UpdateData(
                table: "TrainerAssignments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonalTrainingCharge",
                value: 3000m);

            migrationBuilder.UpdateData(
                table: "TrainerAssignments",
                keyColumn: "Id",
                keyValue: 2,
                column: "PersonalTrainingCharge",
                value: 2500m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalTrainingCharge",
                table: "TrainerAssignments");

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
        }
    }
}
