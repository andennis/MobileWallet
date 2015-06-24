CREATE PROCEDURE [pscn].[GetPassApple]
	@SerialNumber VARCHAR(64),
	@PassTypeId VARCHAR(128)
AS
BEGIN
	SELECT * FROM pscn.Pass p
	INNER JOIN pscn.PassNative pn ON p.PassId = pn.PassId AND DeviceType = 2/*Apple*/
	INNER JOIN pscn.PassApple pa ON pa.PassNativeId = pn.PassNativeId
	WHERE SerialNumber = @SerialNumber
		AND PassTypeId = @PassTypeId
END