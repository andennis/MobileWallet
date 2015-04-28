CREATE VIEW pm.PassContentView
AS 
SELECT 
    pc.*, 
    pct.PassProjectId,
	pct.Name AS PassContentTemplateName 
FROM pm.PassContent pc
INNER JOIN pm.PassContentTemplate pct ON pct.PassContentTemplateId = pc.PassContentTemplateId
