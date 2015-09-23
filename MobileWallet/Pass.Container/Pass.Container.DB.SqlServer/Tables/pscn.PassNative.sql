CREATE TABLE [pscn].[PassNative] (
    [PassNativeId]         INT      IDENTITY (1, 1) NOT NULL,
    [PassId]               INT      NOT NULL,
    [PassFileStorageId]    INT      NULL,
    [DeviceType]           INT      NOT NULL,
    [Version]              ROWVERSION      NOT NULL,
    [CreatedDate]          DATETIME NOT NULL,
    [UpdatedDate]          DATETIME NOT NULL,
    [PassTemplateNativeId] INT      DEFAULT ((0)) NOT NULL,
	[SerialNumber]		   VARCHAR (64) NOT NULL,
    CONSTRAINT [PK_pscn.PassNative] PRIMARY KEY CLUSTERED ([PassNativeId] ASC),
    CONSTRAINT [FK_pscn.PassNative_pscn.Pass_PassId] FOREIGN KEY ([PassId]) REFERENCES [pscn].[Pass] ([PassId]) ON DELETE CASCADE,
    CONSTRAINT [FK_pscn.PassNative_pscn.PassTemplateNative_PassTemplateNativeId] FOREIGN KEY ([PassTemplateNativeId]) REFERENCES [pscn].[PassTemplateNative] ([PassTemplateNativeId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PassTemplateNativeId]
    ON [pscn].[PassNative]([PassTemplateNativeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PassId]
    ON [pscn].[PassNative]([PassId] ASC);

