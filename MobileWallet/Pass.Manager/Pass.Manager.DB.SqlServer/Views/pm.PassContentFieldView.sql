CREATE VIEW [pm].[PassContentFieldView]
AS
SELECT 
	pcf.*, 
	ppf.Name AS FieldName, 
	pctf.FieldKind 
FROM pm.PassContentField pcf
INNER JOIN pm.PassProjectField ppf ON ppf.PassProjectFieldId = pcf.PassProjectFieldId
INNER JOIN pm.PassContent pc ON pcf.PassContentId = pc.PassContentId
INNER JOIN pm.PassContentTemplate pct ON pct.PassContentTemplateId = pc.PassContentTemplateId
LEFT JOIN pm.PassContentTemplateField pctf ON pctf.PassContentTemplateId = pct.PassContentTemplateId
										AND pctf.PassProjectFieldId = pcf.PassProjectFieldId
