CREATE PROCEDURE [pm].[PassContentTemplateField_GetView]
    @ID INT
AS
BEGIN
    SELECT * FROM pm.PassContentTemplateFieldView
    WHERE PassContentTemplateFieldId = @ID
END