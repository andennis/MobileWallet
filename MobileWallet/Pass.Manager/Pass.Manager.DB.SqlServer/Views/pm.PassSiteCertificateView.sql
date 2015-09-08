CREATE VIEW [pm].[PassSiteCertificateView]
AS 
SELECT psc.*, pc.Name, pc.Description, pc.ExpDate, ps.Name AS PassSiteName, ps.Description AS PassSiteDescription FROM pm.PassSiteCertificate psc
INNER JOIN pm.PassCertificate pc ON psc.PassCertificateId = pc.PassCertificateId
INNER JOIN pm.PassSite ps ON psc.PassSiteId = ps.PassSiteId
