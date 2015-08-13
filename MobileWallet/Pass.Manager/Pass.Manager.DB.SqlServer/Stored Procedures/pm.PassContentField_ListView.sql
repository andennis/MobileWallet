CREATE PROCEDURE [pm].[PassContentField_ListView]
	@PassContentId INT
AS
BEGIN
	SELECT * FROM pm.PassContentFieldView
	WHERE PassContentId = @PassContentId
END
