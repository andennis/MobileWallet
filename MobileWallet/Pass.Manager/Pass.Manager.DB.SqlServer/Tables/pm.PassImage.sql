CREATE TABLE [pm].[PassImage] (
    [PassImageId]           INT      IDENTITY (1, 1) NOT NULL,
    [ImageType]             INT      NOT NULL,
    [FileStorageId]         INT      NULL,
    [FileStorage2xId]       INT      NULL,
    [PassContentTemplateId] INT      NOT NULL,
    [Version]               ROWVERSION      NOT NULL,
    [CreatedDate]           DATETIME NOT NULL,
    [UpdatedDate]           DATETIME NOT NULL,
    CONSTRAINT [PK_pm.PassImage] PRIMARY KEY CLUSTERED ([PassImageId] ASC),
    CONSTRAINT [FK_pm.PassImage_pm.PassContentTemplate_PassContentTemplateId] FOREIGN KEY ([PassContentTemplateId]) REFERENCES [pm].[PassContentTemplate] ([PassContentTemplateId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PassContentTemplateId]
    ON [pm].[PassImage]([PassContentTemplateId] ASC);

