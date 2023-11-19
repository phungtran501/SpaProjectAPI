

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


