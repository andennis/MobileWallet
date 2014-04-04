CREATE TABLE [pscn].[PassTemplateApple] (
    [PassTemplateNativeId] INT            NOT NULL,
    [PassTypeId]           NVARCHAR (400) NOT NULL,
	[CertificateStorageItemId] INT            NOT NULL,
    CONSTRAINT [PK_pscn.PassTemplateApple] PRIMARY KEY CLUSTERED ([PassTemplateNativeId] ASC),
    CONSTRAINT [FK_pscn.PassTemplateApple_pscn.PassTemplateNative_PassTemplateNativeId] FOREIGN KEY ([PassTemplateNativeId]) REFERENCES [pscn].[PassTemplateNative] ([PassTemplateNativeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PassTemplateNativeId]
    ON [pscn].[PassTemplateApple]([PassTemplateNativeId] ASC);

