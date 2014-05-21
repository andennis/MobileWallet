CREATE TABLE [pscn].[PassTemplateNative] (
    [PassTemplateNativeId] INT IDENTITY (1, 1) NOT NULL,
    [PassTemplateId]       INT NOT NULL,
    [CertificateId]        INT NOT NULL,
    [DeviceType]           INT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_pscn.PassTemplateNative] PRIMARY KEY CLUSTERED ([PassTemplateNativeId] ASC),
    CONSTRAINT [FK_pscn.PassTemplateNative_pscn.PassTemplate_PassTemplateId] FOREIGN KEY ([PassTemplateId]) REFERENCES [pscn].[PassTemplate] ([PassTemplateId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_PassTemplateId]
    ON [pscn].[PassTemplateNative]([PassTemplateId] ASC);

