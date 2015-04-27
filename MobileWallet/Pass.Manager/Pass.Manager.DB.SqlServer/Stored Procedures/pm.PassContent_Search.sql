CREATE PROCEDURE [pm].[PassContent_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection INT,
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

    @PassProjectId INT
AS
BEGIN
    SELECT * FROM pm.PassContentView
    WHERE PassProjectId = @PassProjectId

    SET @TotalRecords = @@ROWCOUNT
END
