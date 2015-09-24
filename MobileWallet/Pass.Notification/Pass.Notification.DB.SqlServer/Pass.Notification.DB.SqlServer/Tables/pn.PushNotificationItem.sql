CREATE TABLE [pn].[PushNotificationItem](
	[PushNotificationItemId] [int] IDENTITY(1,1) NOT NULL,
	[CertificateStorageId] [int] NOT NULL,
	[PushTockenId] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[PushNotificationServiceType] [int] NOT NULL,
	[Version] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_pn.PushNotificationItem] PRIMARY KEY CLUSTERED ([PushNotificationItemId] ASC)
);


