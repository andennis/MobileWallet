-- =============================================
-- Author:		Denis Andreev
-- Create date: 27.04.2015
-- Description:	
-- =============================================
CREATE PROCEDURE [pm].[PassContentField_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection VARCHAR(4),
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

    @PassContentId INT
AS
BEGIN
    SET @TotalRecords = (SELECT COUNT(*) FROM pm.PassContentFieldView WHERE PassContentId = @PassContentId)

    SELECT * FROM pm.PassContentFieldView WHERE PassContentId = @PassContentId
    ORDER BY 
        CASE WHEN @SortBy = 'PassProjectFieldId'AND @SortDirection = 'asc' THEN PassProjectFieldId END ASC,
        CASE WHEN @SortBy = 'PassProjectFieldId'AND @SortDirection = 'desc' THEN PassProjectFieldId END DESC,
        CASE WHEN @SortBy = 'FieldName'AND @SortDirection = 'asc' THEN FieldName END ASC,
        CASE WHEN @SortBy = 'FieldName'AND @SortDirection = 'desc' THEN FieldName END DESC,
        CASE WHEN @SortBy = 'FieldLabel'AND @SortDirection = 'asc' THEN FieldLabel END ASC,
        CASE WHEN @SortBy = 'FieldLabel'AND @SortDirection = 'desc' THEN FieldLabel END DESC,
        CASE WHEN @SortBy = 'FieldValue'AND @SortDirection = 'asc' THEN FieldValue END ASC,
        CASE WHEN @SortBy = 'FieldValue'AND @SortDirection = 'desc' THEN FieldValue END DESC,
        CASE WHEN @SortBy = 'CreatedDate'AND @SortDirection = 'asc' THEN CreatedDate END ASC,
        CASE WHEN @SortBy = 'CreatedDate'AND @SortDirection = 'desc' THEN CreatedDate END DESC,
        CASE WHEN @SortBy = 'UpdatedDate'AND @SortDirection = 'asc' THEN UpdatedDate END ASC,
        CASE WHEN @SortBy = 'UpdatedDate'AND @SortDirection = 'desc' THEN UpdatedDate END DESC
    OFFSET @PageIndex ROWS FETCH NEXT @PageSize  ROWS ONLY; 
END