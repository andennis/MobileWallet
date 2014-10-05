-- =============================================
-- Author:		Denis Andreev
-- Create date: 5.10.2014
-- Description:	
-- =============================================
CREATE PROCEDURE [pm].[PassSiteUser_GetUnassignedUsers] 
	@PassSiteId INT
AS
BEGIN
	SELECT u.* FROM pm.[User] u
	LEFT JOIN pm.PassSiteUser psu ON u.UserId = psu.UserId AND psu.PassSiteId = @PassSiteId
	WHERE psu.UserId IS NULL
	
END