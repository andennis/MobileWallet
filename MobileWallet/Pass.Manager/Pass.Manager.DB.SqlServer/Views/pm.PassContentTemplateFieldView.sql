CREATE VIEW [pm].[PassContentTemplateFieldView]
AS
SELECT 
    pctf.PassContentTemplateFieldId,
    pctf.FieldKind,
    pctf.AttributedValue,
    pctf.ChangeMessage,
    pctf.TextAlignment,
    pctf.PassProjectFieldId,
    pctf.PassContentTemplateId,
    pctf.[Version],
    pctf.CreatedDate,
    pctf.UpdatedDate,
    ppf.Name,
    (CASE WHEN ppf.PassProjectFieldId IS NOT NULL THEN ppf.DefaultLabel ELSE pctf.Label END) AS Label,
    (CASE WHEN ppf.PassProjectFieldId IS NOT NULL THEN ppf.DefaultValue ELSE pctf.Value END) AS Value
FROM pm.PassContentTemplateField pctf
LEFT JOIN pm.PassProjectField ppf ON pctf.PassProjectFieldId = ppf.PassProjectFieldId
