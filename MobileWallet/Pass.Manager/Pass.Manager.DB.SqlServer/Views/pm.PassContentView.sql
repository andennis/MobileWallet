CREATE VIEW pm.PassContentView
AS 
SELECT 
    pp.PassSiteId,
    pp.PassProjectId,
    pp.Name AS ProjectName,
	pct.Name AS PassContentTemplateName,
    pc.* 
FROM pm.PassProject pp
INNER JOIN pm.PassContentTemplate pct ON pct.PassProjectId = pp.PassProjectId
INNER JOIN pm.PassContent pc ON pc.PassContentTemplateId = pct.PassContentTemplateId
