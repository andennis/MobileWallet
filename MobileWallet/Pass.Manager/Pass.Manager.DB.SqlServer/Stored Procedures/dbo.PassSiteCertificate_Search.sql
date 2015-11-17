-- =============================================
-- Author:		Dima Kovganko
-- Create date: 25.08.2015
-- Description:	
-- =============================================
CREATE PROCEDURE [pm].[PassSiteCertificate_Search]
	@PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection VARCHAR(4),
    @TotalRecords INT OUTPUT,
	@SearchText NVARCHAR(MAX),

	@PassSiteId INT,
	@PassCertificateId INT
AS
BEGIN 
	SELECT * FROM pm.PassSiteCertificateView 
	WHERE (@PassSiteId is null or PassSiteId=@PassSiteId) AND (@PassCertificateId is null or PassCertificateId=@PassCertificateId) 
	ORDER BY  
			CASE WHEN @SortBy = 'PassCertificateId'AND @SortDirection = 'asc' THEN PassCertificateId END ASC,
			CASE WHEN @SortBy = 'PassCertificateId'AND @SortDirection = 'desc' THEN PassCertificateId END DESC,
			CASE WHEN @SortBy = 'Name'AND @SortDirection = 'asc' THEN Name END ASC,
			CASE WHEN @SortBy = 'Name'AND @SortDirection = 'desc' THEN Name END DESC,
			CASE WHEN @SortBy = 'Description'AND @SortDirection = 'asc' THEN Description END ASC,
			CASE WHEN @SortBy = 'Description'AND @SortDirection = 'desc' THEN Description END DESC,
			CASE WHEN @SortBy = 'ExpDate'AND @SortDirection = 'asc' THEN ExpDate END ASC,
			CASE WHEN @SortBy = 'ExpDate'AND @SortDirection = 'desc' THEN ExpDate END DESC
	OFFSET @PageIndex ROWS 
	FETCH NEXT @PageSize  ROWS ONLY;
	SET @TotalRecords = (SELECT COUNT(*) FROM pm.PassSiteCertificateView WHERE (@PassSiteId is null or PassSiteId=@PassSiteId) AND (@PassCertificateId is null or PassCertificateId=@PassCertificateId))
END