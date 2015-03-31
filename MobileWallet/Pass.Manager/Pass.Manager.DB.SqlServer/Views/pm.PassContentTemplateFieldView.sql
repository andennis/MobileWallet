CREATE VIEW [pm].[PassContentTemplateFieldView]
AS
SELECT pctf.*, ppf.Name AS FieldName FROM pm.PassContentTemplateField pctf
INNER JOIN pm.PassProjectField ppf ON pctf.PassProjectFieldId = ppf.PassProjectFieldId
