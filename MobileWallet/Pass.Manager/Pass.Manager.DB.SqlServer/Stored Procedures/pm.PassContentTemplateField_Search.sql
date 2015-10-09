CREATE PROCEDURE [pm].[PassContentTemplateField_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection INT,
    @TotalRecords INT OUTPUT,

    @SearchText NVARCHAR(MAX),
    @PassContentTemplateId INT
AS
BEGIN
    SELECT *, ROW_NUMBER() over (ORDER BY @PassContentTemplateId) AS RowNumber 
		FROM pm.PassContentTemplateFieldView 
		WHERE PassContentTemplateId = @PassContentTemplateId 
		ORDER BY PassContentTemplateId
		OFFSET @PageIndex ROWS
		FETCH NEXT @PageSize ROWS ONLY;
    SET @TotalRecords = (SELECT COUNT(*) FROM pm.PassContentTemplateFieldView WHERE PassContentTemplateId = @PassContentTemplateId)
END