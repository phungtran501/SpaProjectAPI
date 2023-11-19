
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

