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
        CASE WHEN @SortBy = 'IsOnline'AND @SortDirection = 'asc' THEN IsOnline END ASC,
        CASE WHEN @SortBy = 'IsOnline'AND @SortDirection = 'desc' THEN IsOnline END DESC,
        CASE WHEN @SortBy = 'ProjectName'AND @SortDirection = 'asc' THEN ProjectName END ASC,
        CASE WHEN @SortBy = 'ProjectName'AND @SortDirection = 'desc' THEN ProjectName END DESC,
        CASE WHEN @SortBy = 'PassContentTemplateName'AND @SortDirection = 'asc' THEN PassContentTemplateName END ASC,
        CASE WHEN @SortBy = 'PassContentTemplateName'AND @SortDirection = 'desc' THEN PassContentTemplateName END DESC,
        CASE WHEN @SortBy = 'SerialNumber'AND @SortDirection = 'asc' THEN SerialNumber END ASC,
        CASE WHEN @SortBy = 'SerialNumber'AND @SortDirection = 'desc' THEN SerialNumber END DESC,
        CASE WHEN @SortBy = 'ExpDate'AND @SortDirection = 'asc' THEN ExpDate END ASC,
        CASE WHEN @SortBy = 'ExpDate'AND @SortDirection = 'desc' THEN ExpDate END DESC,
        CASE WHEN @SortBy = 'IsVoided'AND @SortDirection = 'asc' THEN IsVoided END ASC,
        CASE WHEN @SortBy = 'IsVoided'AND @SortDirection = 'desc' THEN IsVoided END DESC,
        CASE WHEN @SortBy = 'Status'AND @SortDirection = 'asc' THEN [Status] END ASC,
        CASE WHEN @SortBy = 'Status'AND @SortDirection = 'desc' THEN [Status] END DESC
    OFFSET @PageIndex ROWS FETCH NEXT @PageSize  ROWS ONLY
    
END