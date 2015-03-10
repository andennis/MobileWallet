CREATE PROCEDURE [pm].[PassContentTemplateField_GetUnmappedFields]
	@PassContentTemplateId INT
AS
BEGIN
    SELECT * FROM pm.PassProjectField pf
    INNER JOIN pm.PassContentTemplate ct ON ct.PassProjectId = pf.PassProjectId
    LEFT JOIN pm.PassContentTemplateField ctf ON ctf.PassContentTemplateId = ct.PassContentTemplateId 
                                            AND ctf.PassProjectFieldId = pf.PassProjectFieldId
    WHERE ct.PassContentTemplateId = @PassContentTemplateId
        AND ctf.PassContentTemplateFieldId IS NULL
END