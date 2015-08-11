CREATE VIEW [pm].[PassContentFieldView]
AS
SELECT 
	pc_ppf.PassContentId,
	ppf.PassProjectFieldId,
	ppf.Name AS FieldName, 
	pcf.PassContentFieldId,
	ISNULL(pcf.FieldLabel, ppf.DefaultLabel) AS FieldLabel,
	ISNULL(pcf.FieldValue, ppf.DefaultValue) AS FieldValue,
	pcf.CreatedDate,
	pcf.UpdatedDate,
	STUFF((SELECT ',' + CAST(pctf.FieldKind AS VARCHAR(20)) 
			FROM pm.PassContentTemplateField pctf 
			WHERE pctf.PassProjectFieldId = pc_ppf.PassProjectFieldId AND pctf.PassContentTemplateId = pc_ppf.PassContentTemplateId
			FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 1, '') AS FieldKindIds
FROM 
(
	SELECT DISTINCT ppf.PassProjectFieldId, pct.PassContentId, pctf.PassContentTemplateId
	FROM pm.PassContentTemplateField pctf
	INNER JOIN pm.PassProjectField ppf ON ppf.PassProjectFieldId = pctf.PassProjectFieldId
	INNER JOIN pm.PassContent pct ON pct.PassContentTemplateId = pctf.PassContentTemplateId
) pc_ppf
INNER JOIN pm.PassProjectField ppf ON ppf.PassProjectFieldId = pc_ppf.PassProjectFieldId
LEFT JOIN pm.PassContentField pcf ON pcf.PassProjectFieldId = pc_ppf.PassProjectFieldId AND pcf.PassContentId = pc_ppf.PassContentId 
