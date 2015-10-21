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

-- Create a variable @SQLStatement
DECLARE @SQLStatement NVARCHAR(500);
DECLARE @ParmDefinition NVARCHAR(200);
    
-- Enter the dynamic SQL statement into the
-- variable @SQLStatement
SET @SQLStatement =	   'BEGIN ' +
					   'SELECT * FROM pm.PassSiteCertificateView ' +
					   'WHERE (@PassSiteId is null or PassSiteId=@PassSiteId) AND (@PassCertificateId is null or PassCertificateId=@PassCertificateId) ' +
                       'ORDER BY ' + @SortBy + ' ' + @SortDirection + ' ' +
					   'OFFSET @PageIndex ROWS ' +
					   'FETCH NEXT @PageSize  ROWS ONLY; ' +
					   'SET @TotalRecords = (SELECT COUNT(*) FROM pm.PassSiteCertificateView WHERE (@PassSiteId is null or PassSiteId=@PassSiteId) AND (@PassCertificateId is null or PassCertificateId=@PassCertificateId)) ' +
					   'END';

SET @ParmDefinition = '@PageIndex INT,
					   @PageSize INT,
					   @TotalRecords INT OUTPUT,

					   @PassSiteId INT,
					   @PassCertificateId INT';

-- Execute the SQL statement
EXECUTE sp_executesql @SQLStatement, @ParmDefinition, 
					  @PassSiteId = @PassSiteId, 
					  @PassCertificateId = @PassCertificateId, 
				      @PageIndex = @PageIndex, 
					  @PageSize = @PageSize, 
					  @TotalRecords = @TotalRecords OUTPUT;