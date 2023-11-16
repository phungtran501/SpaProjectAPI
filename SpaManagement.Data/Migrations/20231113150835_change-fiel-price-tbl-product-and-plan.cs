using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpaManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class changefielpricetblproductandplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Product",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Plan",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Plan",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

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
        }
    }
}
