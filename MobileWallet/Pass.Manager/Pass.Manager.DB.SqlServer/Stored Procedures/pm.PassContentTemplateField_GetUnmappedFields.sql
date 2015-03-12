CREATE PROCEDURE [pm].[PassContentTemplateField_GetUnmappedFields]
	@PassContentTemplateId INT,
	@CurPassProjectFieldId INT = NULL
AS
BEGIN
	SELECT * FROM 
	(
		SELECT pf.* FROM pm.PassProjectField pf
		INNER JOIN pm.PassContentTemplate ct ON ct.PassProjectId = pf.PassProjectId
		LEFT JOIN pm.PassContentTemplateField ctf ON ctf.PassContentTemplateId = ct.PassContentTemplateId 
												AND ctf.PassProjectFieldId = pf.PassProjectFieldId
		WHERE ct.PassContentTemplateId = @PassContentTemplateId
			AND ctf.PassContentTemplateFieldId IS NULL
		UNION ALL
		SELECT * FROM pm.PassProjectField pf WHERE PassProjectFieldId = @CurPassProjectFieldId
	)t
	ORDER BY t.Name
	      
END