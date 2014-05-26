--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[pscn].[GetPassSerialNumbersApple]') AND type in (N'P', N'PC'))
--DROP PROCEDURE [pscn].[GetPassSerialNumbersApple]
--GO

-- =============================================
-- Author:		Denis Andreev
-- Create date: 05/12/2014
-- Description:	returns serila numbers of changed passes
-- =============================================
CREATE PROCEDURE [pscn].[GetPassSerialNumbersApple]
    @DeviceId VARCHAR(64),
    @PassTypeId VARCHAR(128),
    @UpdateTag DATETIME = NULL
AS
BEGIN
    SELECT p.SerialNumber FROM pscn.ClientDevice cd
    INNER JOIN pscn.ClientDeviceApple cda ON cd.ClientDeviceId = cda.ClientDeviceId
    INNER JOIN pscn.Registration rg ON rg.ClientDeviceId = cd.ClientDeviceId AND rg.[Status] = 1/*Active*/
    INNER JOIN pscn.Pass p ON p.PassId = rg.PassId AND p.[Status] = 1/*Active*/
    WHERE cd.DeviceId = @DeviceId 
        AND p.PassTypeId = @PassTypeId 
        AND (@UpdateTag IS NULL OR p.UpdatedDate > @UpdateTag)
END