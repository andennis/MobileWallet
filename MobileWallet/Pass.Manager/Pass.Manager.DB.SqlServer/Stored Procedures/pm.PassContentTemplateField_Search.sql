CREATE PROCEDURE [pm].[PassContentTemplateField_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection VARCHAR(4),
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

    @PassContentTemplateId INT
AS

-- Create a variable @SQLStatement
DECLARE @SQLStatement NVARCHAR(500);
DECLARE @ParmDefinition NVARCHAR(200);
    
-- Enter the dynamic SQL statement into the
-- variable @SQLStatement
SET @SQLStatement =	   'BEGIN ' +
					   'SELECT * FROM pm.PassContentTemplateFieldView ' +
					   'WHERE PassContentTemplateId = @PassContentTemplateId ' +
                       'ORDER BY ' + @SortBy + ' ' + @SortDirection + ' ' +
					   'OFFSET @PageIndex ROWS ' +
					   'FETCH NEXT @PageSize  ROWS ONLY; ' +
					   'SET @TotalRecords = (SELECT COUNT(*) FROM pm.PassContentTemplateFieldView WHERE PassContentTemplateId = @PassContentTemplateId) ' +
					   'END';

SET @ParmDefinition = '@PageIndex INT,
					   @PageSize INT,
					   @TotalRecords INT OUTPUT,

					   @PassContentTemplateId INT';

-- Execute the SQL statement
EXECUTE sp_executesql @SQLStatement, @ParmDefinition, 
					  @PassContentTemplateId = @PassContentTemplateId, 
				      @PageIndex = @PageIndex, 
					  @PageSize = @PageSize, 
					  @TotalRecords = @TotalRecords OUTPUT;