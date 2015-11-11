CREATE PROCEDURE [pm].[PassContent_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection VARCHAR(4),
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

    @PassSiteId INT,
    @PassProjectId INT,
    @PassContentTemplateId INT
AS
BEGIN
    SET @TotalRecords = 
        (
            SELECT COUNT(*) FROM pm.PassContentView 
                WHERE PassSiteId = @PassSiteId
                    AND (@PassProjectId IS NULL OR PassProjectId = @PassProjectId)
                    AND (@PassContentTemplateId IS NULL OR PassContentTemplateId = @PassContentTemplateId)
        )

    SELECT * FROM pm.PassContentView
    WHERE PassSiteID = @PassSiteId
        AND (@PassProjectId IS NULL OR PassProjectId = @PassProjectId)
        AND (@PassContentTemplateId IS NULL OR PassContentTemplateId = @PassContentTemplateId)
    ORDER BY 
        CASE WHEN @SortBy = 'PassContentId'AND @SortDirection = 'asc' THEN PassContentId END ASC,
        CASE WHEN @SortBy = 'PassContentId'AND @SortDirection = 'desc' THEN PassContentId END DESC,
        CASE WHEN @SortBy = 'SerialNumber'AND @SortDirection = 'asc' THEN SerialNumber END ASC,
        CASE WHEN @SortBy = 'SerialNumber'AND @SortDirection = 'desc' THEN SerialNumber END DESC,
        CASE WHEN @SortBy = 'ExpDate'AND @SortDirection = 'asc' THEN ExpDate END ASC,
        CASE WHEN @SortBy = 'ExpDate'AND @SortDirection = 'desc' THEN ExpDate END DESC
    OFFSET @PageIndex ROWS FETCH NEXT @PageSize  ROWS ONLY
    
END