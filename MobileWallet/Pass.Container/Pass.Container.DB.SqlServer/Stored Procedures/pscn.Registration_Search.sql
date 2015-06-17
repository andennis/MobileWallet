CREATE PROCEDURE [pscn].[Registration_Search]
    @PageIndex INT,
    @PageSize INT,
    @SortBy VARCHAR(64),
    @SortDirection INT,
    @TotalRecords INT OUTPUT,
    @SearchText NVARCHAR(MAX),

	@PassId INT,
	@StatusId INT
AS
BEGIN
    SELECT * FROM pscn.RegistrationView
    WHERE PassId = @PassId 
		AND ([Status] = @StatusId OR @StatusId IS NULL)

    SET @TotalRecords = @@ROWCOUNT
END
