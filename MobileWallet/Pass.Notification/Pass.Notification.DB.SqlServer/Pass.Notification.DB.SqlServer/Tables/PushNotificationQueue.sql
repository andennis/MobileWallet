CREATE TABLE [pn].[PushNotificationQueue]
(	
    [PushNotificationId] INT IDENTITY (1, 1) NOT NULL,
    [CertificateStorageId] INT NOT NULL,
    [PushTockenId]  NVARCHAR(400) NOT NULL,
    [CreatedDate]  DATETIME NOT NULL,
    [UpdatedDate]  DATETIME NOT NULL,
    [Status]  INT NOT NULL,
	[PushNotificationServiceType]  INT NOT NULL
);

