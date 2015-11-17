CREATE PROCEDURE [pm].[PassContentTemplateField_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection VARCHAR(4),
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

    @PassContentTemplateId INT
AS
BEGIN
	SELECT * FROM pm.PassContentTemplateFieldView 
	WHERE PassContentTemplateId = @PassContentTemplateId 
	ORDER BY  
		CASE WHEN @SortBy = 'PassContentTemplateFieldId'AND @SortDirection = 'asc' THEN PassContentTemplateFieldId END ASC,
        CASE WHEN @SortBy = 'PassContentTemplateFieldId'AND @SortDirection = 'desc' THEN PassContentTemplateFieldId END DESC,
		CASE WHEN @SortBy = 'IsStatic'AND @SortDirection = 'asc' THEN PassProjectFieldId END ASC,
        CASE WHEN @SortBy = 'IsStatic'AND @SortDirection = 'desc' THEN PassProjectFieldId END DESC,
		CASE WHEN @SortBy = 'FieldKind'AND @SortDirection = 'asc' THEN FieldKind END ASC,
        CASE WHEN @SortBy = 'FieldKind'AND @SortDirection = 'desc' THEN FieldKind END DESC,
		CASE WHEN @SortBy = 'Name'AND @SortDirection = 'asc' THEN Name END ASC,
        CASE WHEN @SortBy = 'Name'AND @SortDirection = 'desc' THEN Name END DESC,
		CASE WHEN @SortBy = 'Label'AND @SortDirection = 'asc' THEN Label END ASC,
        CASE WHEN @SortBy = 'Label'AND @SortDirection = 'desc' THEN Label END DESC,
		CASE WHEN @SortBy = 'Value'AND @SortDirection = 'asc' THEN Value END ASC,
        CASE WHEN @SortBy = 'Value'AND @SortDirection = 'desc' THEN Value END DESC
	OFFSET @PageIndex ROWS FETCH NEXT @PageSize  ROWS ONLY; 
	SET @TotalRecords = (SELECT COUNT(*) FROM pm.PassContentTemplateFieldView WHERE PassContentTemplateId = @PassContentTemplateId) 
END