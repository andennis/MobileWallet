CREATE PROCEDURE [pm].[PassContent_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection INT,
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

    @PassSiteId INT,
    @PassProjectId INT,
    @PassContentTemplateId INT
AS
BEGIN
    SELECT * FROM pm.PassContentView
    WHERE PassContentTemplateId = @PassContentTemplateId
    /*
    WHERE PassSiteID = @PassSiteId
        AND (@PassProjectId IS NULL OR PassProjectId = @PassProjectId)
        AND (@PassContentTemplateId IS NULL OR PassContentTemplateId = @PassContentTemplateId)
        */

    SET @TotalRecords = @@ROWCOUNT
END
