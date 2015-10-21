-- =============================================
-- Author:		Denis Andreev
-- Create date: 27.04.2015
-- Description:	
-- =============================================
CREATE PROCEDURE [pm].[PassContentField_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection VARCHAR(4),
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

    @PassContentId INT
AS

-- Create a variable @SQLStatement
DECLARE @SQLStatement NVARCHAR(500);
DECLARE @ParmDefinition NVARCHAR(200);
    
-- Enter the dynamic SQL statement into the
-- variable @SQLStatement
SET @SQLStatement =	   'BEGIN ' +
					   'SELECT * FROM pm.PassContentFieldView ' +
					   'WHERE PassContentId = @PassContentId ' +
                       'ORDER BY ' + @SortBy + ' ' + @SortDirection + ' ' +
					   'OFFSET @PageIndex ROWS ' +
					   'FETCH NEXT @PageSize  ROWS ONLY; ' +
					   'SET @TotalRecords = (SELECT COUNT(*) FROM pm.PassContentFieldView WHERE PassContentId = @PassContentId) ' +
					   'END';

SET @ParmDefinition = '@PageIndex INT,
					   @PageSize INT,
					   @TotalRecords INT OUTPUT,

					   @PassContentId INT';

-- Execute the SQL statement
EXECUTE sp_executesql @SQLStatement, @ParmDefinition, 
					  @PassContentId = @PassContentId, 
				      @PageIndex = @PageIndex, 
					  @PageSize = @PageSize, 
					  @TotalRecords = @TotalRecords OUTPUT;