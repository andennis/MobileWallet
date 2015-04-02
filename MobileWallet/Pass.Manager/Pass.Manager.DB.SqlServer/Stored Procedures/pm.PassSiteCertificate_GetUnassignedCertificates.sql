-- =============================================
-- Author:		Vitaly Kovganko
-- Create date: 10.10.2014
-- Description:	
-- =============================================
CREATE PROCEDURE [pm].[PassSiteCertificate_GetUnassignedCertificates] 
	@PassSiteId INT
AS
BEGIN
	SELECT c.* FROM pm.[PassCertificate] c
	LEFT JOIN pm.PassSiteCertificate psc ON c.PassCertificateId = psc.PassCertificateId AND psc.PassSiteId = @PassSiteId
	WHERE psc.PassCertificateId IS NULL
	
END