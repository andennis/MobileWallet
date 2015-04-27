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

    SET @TotalRecords = @@ROWCOUNT
END