CREATE VIEW pm.PassContentView
AS 
SELECT 
    pp.PassSiteId,
    pp.PassProjectId,
    pp.Name AS ProjectName,
	pct.Name AS PassContentTemplateName,
    CAST((CASE WHEN pc.ContainerPassId IS NULL THEN 0 ELSE 1 END) AS BIT) AS IsOnline,
    pc.* 
FROM pm.PassProject pp
INNER JOIN pm.PassContentTemplate pct ON pct.PassProjectId = pp.PassProjectId
INNER JOIN pm.PassContent pc ON pc.PassContentTemplateId = pct.PassContentTemplateId
