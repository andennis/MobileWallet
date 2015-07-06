CREATE PROCEDURE [pscn].[GetPassApple]
	@SerialNumber VARCHAR(64),
	@PassTypeId VARCHAR(128)
AS
BEGIN
	SELECT p.* FROM pscn.Pass p
	INNER JOIN pscn.PassNative pn ON p.PassId = pn.PassId AND pn.DeviceType = 2/*Apple*/
	INNER JOIN pscn.PassApple pa ON pa.PassNativeId = pn.PassNativeId
	WHERE p.SerialNumber = @SerialNumber
		AND pa.PassTypeId = @PassTypeId
END