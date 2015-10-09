-- =============================================
-- Author:		Denis Andreev
-- Create date: 27.04.2015
-- Description:	
-- =============================================
CREATE PROCEDURE [pm].[PassContentField_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection INT,
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

    @PassContentId INT
AS
BEGIN
    SELECT * FROM pm.PassContentFieldView
    WHERE PassContentId = @PassContentId
	ORDER BY PassContentId
	OFFSET @PageIndex ROWS
	FETCH NEXT @PageSize ROWS ONLY;
    SET @TotalRecords = (SELECT COUNT(*) FROM pm.PassContentFieldView WHERE PassContentId = @PassContentId)
END