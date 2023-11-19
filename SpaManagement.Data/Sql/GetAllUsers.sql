
CREATE PROCEDURE GetAllUsers
	@pageIndex INT,
	@pageSize INT,
	@totalRecord INT OUT
AS
BEGIN

	SELECT  app.Id, app.UserName, app.Fullname, app.Email, app.PhoneNumber, app.Address, app.IsActive, app.IsSystem , rol.Name as RoleName,
			ROW_NUMBER() OVER(ORDER BY app.UserName) as RowNo INTO #TBUSER
	FROM ApplicationUser app LEFT JOIN UserRole userrole ON app.Id = userrole.UserId
							 LEFT JOIN [Role] rol ON userrole.RoleId = rol.Id
	WHERE app.UserName <> 'Administrator'
		AND app.IsActive = 1

	SELECT @totalRecord = COUNT(*) FROM #TBUSER

	SELECT * 
	FROM #TBUSER
	WHERE RowNo BETWEEN @pageIndex AND @pageSize
	
	DROP TABLE #TBUSER

END