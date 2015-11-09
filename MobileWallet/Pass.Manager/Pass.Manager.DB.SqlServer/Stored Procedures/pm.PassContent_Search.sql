CREATE PROCEDURE [pm].[PassContent_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection VARCHAR(4),
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

    @PassSiteId INT,
    @PassProjectId INT,
    @PassContentTemplateId INT
AS

-- Create a variable @SQLStatement
DECLARE @SQLStatement NVARCHAR(500);
DECLARE @ParmDefinition NVARCHAR(200);
    
-- Enter the dynamic SQL statement into the
-- variable @SQLStatement
SET @SQLStatement =	   'BEGIN ' +
					   'SELECT * FROM pm.PassContentView ' +
					   'WHERE PassSiteId = @PassSiteId ' +
                       'ORDER BY ' + @SortBy + ' ' + @SortDirection + ' ' +
					   'OFFSET @PageIndex ROWS ' +
					   'FETCH NEXT @PageSize  ROWS ONLY; ' +
					   'SET @TotalRecords = (SELECT COUNT(*) FROM pm.PassContentView WHERE PassSiteId = @PassSiteId) ' +
					   'END';

SET @ParmDefinition = '@PageIndex INT,
					   @PageSize INT,
					   @TotalRecords INT OUTPUT,

					   @PassSiteId INT';

-- Execute the SQL statement
EXECUTE sp_executesql @SQLStatement, @ParmDefinition, 
					  @PassSiteId = @PassSiteId,
				      @PageIndex = @PageIndex, 
					  @PageSize = @PageSize, 
					  @TotalRecords = @TotalRecords OUTPUT;

					   /*
    WHERE PassSiteID = @PassSiteId
        AND (@PassProjectId IS NULL OR PassProjectId = @PassProjectId)
        AND (@PassContentTemplateId IS NULL OR PassContentTemplateId = @PassContentTemplateId)
        */