CREATE TABLE [pm].[PassBarcode] (
    [PassContentTemplateId] INT            NOT NULL,
    [AltText]               NVARCHAR (MAX) NULL,
    [Format]                INT            NOT NULL,
    [Message]               NVARCHAR (64)  NOT NULL,
    [MessageEncoding]       NVARCHAR (32)  NULL,
    [Version]               INT            NOT NULL,
    [CreatedDate]           DATETIME       NOT NULL,
    [UpdatedDate]           DATETIME       NOT NULL,
    CONSTRAINT [PK_pm.PassBarcode] PRIMARY KEY CLUSTERED ([PassContentTemplateId] ASC),
    CONSTRAINT [FK_pm.PassBarcode_pm.PassContentTemplate_PassContentTemplateId] FOREIGN KEY ([PassContentTemplateId]) REFERENCES [pm].[PassContentTemplate] ([PassContentTemplateId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PassContentTemplateId]
    ON [pm].[PassBarcode]([PassContentTemplateId] ASC);

