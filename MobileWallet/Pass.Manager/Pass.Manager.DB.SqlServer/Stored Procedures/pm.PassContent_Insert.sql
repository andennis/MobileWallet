CREATE PROCEDURE [pm].[PassContent_Insert]
	@PassContentId INT OUTPUT,
	@SerialNumber VARCHAR(64),
	@AuthToken VARCHAR(64),
	@ExpDate DATETIME,
	@PassContentTemplateId INT
AS
BEGIN
	DECLARE @dt DATETIME = GETDATE()
	DECLARE @PassProjectId INT
	DECLARE @PassContentTemplateVersion INT
    
	SELECT 
		@PassProjectId = PassProjectId,
		@PassContentTemplateVersion = [Version]
	FROM pm.PassContentTemplate 
	WHERE PassContentTemplateId = @PassContentTemplateId

	INSERT INTO pm.PassContent
	( 
		SerialNumber,
		AuthToken,
		ExpDate,
		IsVoided,
	    PassContentTemplateId,
		PassContentTemplateVersion,
	    [Status],
	    [Version],
	    CreatedDate,
	    UpdatedDate
	)
	VALUES  
	( 
		@SerialNumber,
		@AuthToken,
		@ExpDate,
		0,
	    @PassContentTemplateId,
		@PassContentTemplateVersion,
	    1/*Active*/,
	    1,
	    @dt,
	    @dt
	)

	SET @PassContentId = SCOPE_IDENTITY()
	

	INSERT INTO pm.PassContentField
	( 
		PassContentId,
		PassProjectFieldId,
	    FieldLabel,
	    FieldValue,
	    [Version],
	    CreatedDate,
	    UpdatedDate
	)
	SELECT 
		@PassContentId, 
		PassProjectFieldId,
		DefaultLabel,
		DefaultValue,
		1,
		@dt,
		@dt
	FROM pm.PassProjectField
	WHERE PassProjectId = @PassProjectId

END