using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpaManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class addstoreprocedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetAllAppointment')
                                    BEGIN
                                        EXEC('
                                                CREATE PROCEDURE GetAllAppointment
													@pageIndex INT,
													@pageSize INT,
													@totalRecord INT OUT
												AS
												BEGIN

													SELECT  appo.Id, app.UserName, appo.CreatedOn, appo.Note, appo.[Status],
															ROW_NUMBER() OVER(ORDER BY appo.Id) as RowNo INTO #TBAPPOINTMENT
													FROM Appointment appo LEFT JOIN ApplicationUser app ON appo.UserId = app.Id		
																				AND app.IsActive <> 0 
																				AND app.IsSystem <> 1 
	
													SELECT @totalRecord = COUNT(*) FROM #TBAPPOINTMENT

													SELECT * 
													FROM #TBAPPOINTMENT
													WHERE RowNo BETWEEN @pageIndex AND @pageSize
	
													DROP TABLE #TBAPPOINTMENT
												END
																						');
																					END
								");

            migrationBuilder.Sql($@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetAllProducts')
                                    BEGIN
                                        EXEC('
                                                CREATE PROCEDURE GetAllProducts
													@pageIndex INT,
													@pageSize INT,
													@totalRecord INT OUT
												AS
												BEGIN

													SELECT  pr.Id, sv.Name as ServiceName ,pr.Name, pr.Decription, pr.CreateOn, pr.Price, pr.IsActive,
															ROW_NUMBER() OVER(ORDER BY pr.Id) as RowNo INTO #TBPRODUCT
													FROM Product pr LEFT JOIN [Services] sv ON pr.ServicesId = sv.Id
													WHERE  pr.IsActive = 1

													SELECT @totalRecord = COUNT(*) FROM #TBPRODUCT

													SELECT * 
													FROM #TBPRODUCT
													WHERE RowNo BETWEEN @pageIndex AND @pageSize
	
													DROP TABLE #TBPRODUCT

												END
																						');
																					END
													");

			var storedUser = @" PROCEDURE GetAllUsers
									@pageIndex INT,
									@pageSize INT,
									@totalRecord INT OUT
								AS
								BEGIN

									SELECT  app.Id, app.UserName, app.Fullname, app.Email, app.PhoneNumber, app.Address, app.IsActive, app.IsSystem , rol.Name as RoleName,
											ROW_NUMBER() OVER(ORDER BY app.UserName) as RowNo INTO #TBUSER
									FROM ApplicationUser app LEFT JOIN UserRole userrole ON app.Id = userrole.UserId
																LEFT JOIN [Role] rol ON userrole.RoleId = rol.Id
									WHERE app.UserName <> ''Admin''
										AND app.IsActive = 1

									SELECT @totalRecord = COUNT(*) FROM #TBUSER

									SELECT * 
									FROM #TBUSER
									WHERE RowNo BETWEEN @pageIndex AND @pageSize
	
									DROP TABLE #TBUSER

								END";

            migrationBuilder.Sql($@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'GetAllUsers')
										BEGIN
											EXEC('
													CREATE{storedUser}
											');
										END

									ELSE
										BEGIN
											EXEC('
													ALTER {storedUser}
											');
										END

							");


        }

        /// <inheritdoc />

    }
}
