CREATE PROCEDURE pm.PassContentField_GetView
	@ID INT
AS
BEGIN
    SELECT * FROM pm.PassContentFieldView
    WHERE PassContentFieldId = @ID
END