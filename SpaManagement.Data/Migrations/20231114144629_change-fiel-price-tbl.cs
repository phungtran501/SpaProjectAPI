using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpaManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class changefielpricetbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "2e2e2086-ae85-4981-813c-1c79dedd7e0f");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5667f3ff-8c91-4de6-9dbb-2f4a6f95d044", "f36cfc3d-5ee3-42c8-b64c-263328bc486f" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "f36cfc3d-5ee3-42c8-b64c-263328bc486f");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "5667f3ff-8c91-4de6-9dbb-2f4a6f95d044");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Plan",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Appointment",
                newName: "CreatedOn");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "AppointmentProductDetail",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<short>(
                name: "Status",
                table: "Appointment",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "IsActive", "IsSystem", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8464d552-5817-46e3-9f9d-bfc4f702be58", 0, null, "2f0deb0b-d23f-4697-8f8c-ec1c5d051543", "admin@gmail.com", false, null, false, false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEHOHeme/7YNCsNZGCPBAr1kv4s4ARi5oyOsfy6O2hD/66bLc4F/J4irU1OrwYHDQtQ==", null, false, "a56e74c4-ef48-4373-bc6e-e6c0afa75062", false, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "88bf0e59-744a-4eb8-966b-28d9defac73e", "ae5b6716-f6bd-41b6-80cd-4a97944045e1", "User", "USER" },
                    { "f7a32352-7282-48a2-93c8-22d023ff5835", "ed6b0081-2f32-46fa-8089-253673e18536", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f7a32352-7282-48a2-93c8-22d023ff5835", "8464d552-5817-46e3-9f9d-bfc4f702be58" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "88bf0e59-744a-4eb8-966b-28d9defac73e");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f7a32352-7282-48a2-93c8-22d023ff5835", "8464d552-5817-46e3-9f9d-bfc4f702be58" });

            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: "8464d552-5817-46e3-9f9d-bfc4f702be58");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "f7a32352-7282-48a2-93c8-22d023ff5835");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Plan",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Appointment",
                newName: "CreateOn");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "AppointmentProductDetail",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "IsActive", "IsSystem", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f36cfc3d-5ee3-42c8-b64c-263328bc486f", 0, null, "cd3b6044-9ef5-4999-b8db-2622899cef20", "admin@gmail.com", false, null, false, false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEOLNtdXrWJU80vntY7HgY/f7R15d2/L4GtR/u4lsvM/qvu3fjJ+3eOH3eJV0r3oAWw==", null, false, "b7942aa7-2ede-4336-b16d-0cb1ca5eb296", false, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e2e2086-ae85-4981-813c-1c79dedd7e0f", "376da4a4-15a3-4e45-992a-214ae868663e", "User", "USER" },
                    { "5667f3ff-8c91-4de6-9dbb-2f4a6f95d044", "963ff038-0316-49b8-9183-2409d4d1defc", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5667f3ff-8c91-4de6-9dbb-2f4a6f95d044", "f36cfc3d-5ee3-42c8-b64c-263328bc486f" });
        }
    }
}
