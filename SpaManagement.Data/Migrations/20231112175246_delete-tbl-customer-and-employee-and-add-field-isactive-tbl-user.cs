using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpaManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class deletetblcustomerandemployeeandaddfieldisactivetbluser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Customer_CustomerId",
                table: "Appointment");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_CustomerId",
                table: "Appointment");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "3266d99a-61c7-4081-aa9d-f42b13385087");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "97d131a6-d929-40e4-8736-08f41ef6100d", "0a3526dc-1fc2-4860-a078-67cdf725b28b" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "0a3526dc-1fc2-4860-a078-67cdf725b28b");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "97d131a6-d929-40e4-8736-08f41ef6100d");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ApplicationUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                table: "ApplicationUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "IsActive", "IsSystem", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c235e437-15a4-4985-a59c-93507827f184", 0, null, "47cfaab0-6b80-4cd7-ae94-55a7387b5e05", "admin@gmail.com", false, null, false, false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEPLKZ6cSk0EyTi3cYlVdp4v2xBxFZVsXpYusHWf1SqNgp0yMES3Ne/0PErlECHeThw==", null, false, "fc77efb6-2cc8-40c5-88d9-be5ae8bfd8aa", false, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e368ff13-09f5-4c53-9e96-26cc2ae06521", "e87017e6-9be8-402c-93be-993a144e34ba", "Admin", "ADMIN" },
                    { "eb9b5cb2-09ad-49ab-83d2-40df73379f07", "4325155c-fbbe-40e4-aab9-cdc43f2befc0", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e368ff13-09f5-4c53-9e96-26cc2ae06521", "c235e437-15a4-4985-a59c-93507827f184" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_ApplicationUser_UserId",
                table: "Appointment",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_ApplicationUser_UserId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_UserId",
                table: "Appointment");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "eb9b5cb2-09ad-49ab-83d2-40df73379f07");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e368ff13-09f5-4c53-9e96-26cc2ae06521", "c235e437-15a4-4985-a59c-93507827f184" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "c235e437-15a4-4985-a59c-93507827f184");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "e368ff13-09f5-4c53-9e96-26cc2ae06521");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "IsSystem",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0a3526dc-1fc2-4860-a078-67cdf725b28b", 0, null, "950ef737-60d2-4fe0-92ab-dc4fd6b01805", "admin@gmail.com", false, null, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEAKyIjJTV9qqf8vt9bmtOgOKFe6NZqi/vgH6Elzri+U35JHEHZ/fQGhHeAesxqO3pw==", null, false, "11ac663e-7abb-43dd-be03-d37b1e330431", false, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3266d99a-61c7-4081-aa9d-f42b13385087", "c3421baf-15df-4043-b81a-659d6be4e64e", "User", "USER" },
                    { "97d131a6-d929-40e4-8736-08f41ef6100d", "813e7299-191b-4a0f-9ae9-0ef6ddfcc2df", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "97d131a6-d929-40e4-8736-08f41ef6100d", "0a3526dc-1fc2-4860-a078-67cdf725b28b" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CustomerId",
                table: "Appointment",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Customer_CustomerId",
                table: "Appointment",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
