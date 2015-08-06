CREATE VIEW [pm].[PassContentFieldView]
AS
SELECT 
	pct.PassContentId,
	ppf.PassProjectFieldId,
	ppf.Name AS FieldName, 
	pctf.FieldKind,
	pcf.PassContentFieldId,
	ISNULL(pcf.FieldLabel, ppf.DefaultLabel) AS FieldLabel,
	ISNULL(pcf.FieldValue, ppf.DefaultValue) AS FieldValue,
	pcf.CreatedDate,
	pcf.UpdatedDate
FROM pm.PassContentTemplateField pctf
INNER JOIN pm.PassContent pct ON pctf.PassContentTemplateId = pct.PassContentTemplateId
INNER JOIN pm.PassProjectField ppf ON pctf.PassProjectFieldId = ppf.PassProjectFieldId
LEFT JOIN pm.PassContentField pcf ON ppf.PassProjectFieldId = pcf.PassProjectFieldId AND pct.PassContentId = pcf.PassContentId


