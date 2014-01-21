-- =============================================
-- Author:		Denis Andreev
-- Create date: 01/21/2014
-- Description:	
-- =============================================
CREATE PROCEDURE [fs].[GetFreeFolder] 
	@Level INT,
	@MaxItemsNumber INT
AS
BEGIN
	;WITH folders(FolderItemId, ParentId, Name, [level]) AS
	(
		SELECT FolderItemId, ParentId, Name, 1 AS [level]
		FROM fs.FolderItem where ParentId is null
		UNION all
		SELECT f2.FolderItemId, f2.ParentId, f2.Name, f1.[level] + 1 
		FROM folders f1
		INNER JOIN fs.FolderItem f2 on f1.FolderItemId = f2.ParentId
	)
	SELECT TOP 1 ParentId/*, COUNT(*) AS Number*/ FROM folders 
    WHERE [level] = @Level
	GROUP BY ParentId
	HAVING COUNT(*) < @MaxItemsNumber
	ORDER BY COUNT(*)


END