-- =============================================
-- Author:		Dima Kovganko
-- Create date: 25.08.2015
-- Description:	
-- =============================================
CREATE PROCEDURE [pm].[PassSiteCertificate_Search]
	@PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection INT,
    @TotalRecords INT OUTPUT,
	@SearchText NVARCHAR(MAX),

	@PassSiteId INT,
	@PassCertificateId INT
AS
BEGIN
    SELECT * FROM pm.PassSiteCertificateView
	WHERE (@PassSiteId is null or PassSiteId=@PassSiteId) AND (@PassCertificateId is null or PassCertificateId=@PassCertificateId)

    SET @TotalRecords = @@ROWCOUNT
END
