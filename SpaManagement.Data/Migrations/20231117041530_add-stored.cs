using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpaManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class addstored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;

            var stores = new Dictionary<string, string>()
            {
                {"GetAllUsers", "Sql/GetAllUsers.spl" },
                {"GetAllProducts", "Sql/GetAllProducts.spl" },
                {"GetAllAppointment", "Sql/GetAllAppointment.spl" },
            };

            foreach ( var item in stores )
            {
                var sqlScript = File.ReadAllText(Path.Combine(path, item.Value));

                migrationBuilder.Sql($@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = '{item.Key}')
                                    BEGIN
                                        EXEC sp_excutesql N'{sqlScript}';
                                    END
                                    ");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
