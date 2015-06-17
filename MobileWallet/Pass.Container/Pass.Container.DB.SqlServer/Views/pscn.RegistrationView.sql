CREATE VIEW [pscn].[RegistrationView]
AS 
SELECT 
	rg.*, 
	cd.DeviceId, 
	cd.DeviceType, 
	cda.PushToken 
FROM pscn.Registration rg
INNER JOIN pscn.ClientDevice cd ON rg.ClientDeviceId = cd.ClientDeviceId
INNER JOIN pscn.ClientDeviceApple cda ON cd.ClientDeviceId = cda.ClientDeviceId
