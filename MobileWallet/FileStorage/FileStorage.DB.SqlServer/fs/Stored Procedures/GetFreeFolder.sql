-- =============================================
-- Author:		Denis Andreev
-- Create date: 01/21/2014
-- Description:	
-- =============================================
CREATE PROCEDURE [fs].[GetFreeFolder] 
	@ItemLevel INT,
	@MaxItemsNumber INT
AS
BEGIN
    DECLARE @FolderItemId INT = NULL;

	;WITH folders(FolderItemId, ParentId, ItemLevel) AS
	(
		SELECT FolderItemId, ParentId, 1 AS ItemLevel
		FROM fs.FolderItem WHERE ParentId IS NULL
		UNION ALL
		SELECT f2.FolderItemId, f2.ParentId, f1.ItemLevel + 1 
		FROM folders f1
		INNER JOIN fs.FolderItem f2 on f1.FolderItemId = f2.ParentId
	)
	SELECT TOP 1 @FolderItemId = si.ParentId FROM folders f
    INNER JOIN fs.StorageItem si ON si.ParentId = f.FolderItemId
    WHERE f.ItemLevel = @ItemLevel
	GROUP BY si.ParentId
	HAVING COUNT(*) < @MaxItemsNumber
	ORDER BY COUNT(*)

    SELECT * FROM FolderItem WHERE FolderItemId = @FolderItemId
END