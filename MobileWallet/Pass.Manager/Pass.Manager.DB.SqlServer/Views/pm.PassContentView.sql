CREATE VIEW pm.PassContentView
AS 
SELECT pc.*, pct.PassProjectId FROM pm.PassContent pc
INNER JOIN pm.PassContentTemplate pct ON pct.PassContentTemplateId = pc.PassContentTemplateId
