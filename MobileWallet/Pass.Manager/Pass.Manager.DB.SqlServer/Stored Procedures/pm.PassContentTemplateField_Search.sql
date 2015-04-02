CREATE PROCEDURE [pm].[PassContentTemplateField_Search]
    --@PageIndex INT,
    --@PageSize INT,
    --@SortBy VARCHAR(64),
    --@SortDirection INT,
    --@TotalRecords INT OUTPUT,

    --@SearchText NVARCHAR(MAX),
    @PassContentTemplateId INT
AS
BEGIN
    --SET @TotalRecords = 0

    SELECT * FROM pm.PassContentTemplateFieldView
    WHERE PassContentTemplateId = @PassContentTemplateId

    --SET @TotalRecords = @@ROWCOUNT
END