CREATE PROCEDURE pm.PassContentField_GetView
	@PassContentFieldId INT,
	@PassContentId INT,
	@PassProjectFieldId INT
AS
BEGIN
    SELECT * FROM pm.PassContentFieldView
    WHERE (@PassContentFieldId IS NULL OR PassContentFieldId = @PassContentFieldId)
		AND	(@PassContentId IS NULL OR PassContentId = @PassContentId)
		AND (@PassProjectFieldId IS NULL OR PassProjectFieldId = @PassProjectFieldId)
END