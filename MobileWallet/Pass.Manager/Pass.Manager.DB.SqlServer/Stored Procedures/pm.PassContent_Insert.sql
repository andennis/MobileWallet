ALTER PROCEDURE [pm].[PassContent_Insert]
	@PassContentId INT OUTPUT,
	@SerialNumber VARCHAR(64),
	@AuthToken VARCHAR(64),
	@ExpDate DATETIME,
	@PassContentTemplateId INT
AS
BEGIN
	DECLARE @dt DATETIME = GETDATE()

	INSERT INTO pm.PassContent
	( 
		SerialNumber,
		AuthToken,
		ExpDate,
		IsVoided,
	    PassContentTemplateId,
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
	    1/*Active*/,
	    1,
	    @dt,
	    @dt
	)

	SET @PassContentId = SCOPE_IDENTITY()
	DECLARE @PassProjectId INT = (SELECT PassProjectId FROM pm.PassContentTemplate WHERE PassContentTemplateId = @PassContentTemplateId)

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