CREATE TABLE [pm].[PassContentTemplateField] (
    [PassContentTemplateFieldId] INT            IDENTITY (1, 1) NOT NULL,
    [FieldKind]                  INT            NOT NULL,
    [AttributedValue]            NVARCHAR (128) NULL,
    [ChangeMessage]              NVARCHAR (128) NULL,
    [Label]                      NVARCHAR (128) NULL,
    [TextAlignment]              INT            NULL,
    [PassProjectFieldId]         INT            NULL,
    [PassContentTemplateId]      INT            NOT NULL,
    [Version]                    INT            NOT NULL,
    [CreatedDate]                DATETIME       NOT NULL,
    [UpdatedDate]                DATETIME       NOT NULL,
    [Value]                      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_pm.PassContentTemplateField] PRIMARY KEY CLUSTERED ([PassContentTemplateFieldId] ASC),
    CONSTRAINT [FK_pm.PassContentTemplateField_pm.PassContentTemplate_PassContentTemplateId] FOREIGN KEY ([PassContentTemplateId]) REFERENCES [pm].[PassContentTemplate] ([PassContentTemplateId]) ON DELETE CASCADE,
    CONSTRAINT [FK_pm.PassContentTemplateField_pm.PassProjectField_PassProjectFieldId] FOREIGN KEY ([PassProjectFieldId]) REFERENCES [pm].[PassProjectField] ([PassProjectFieldId])
);




GO
CREATE NONCLUSTERED INDEX [IX_PassContentTemplateId]
    ON [pm].[PassContentTemplateField]([PassContentTemplateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PassProjectFieldId]
    ON [pm].[PassContentTemplateField]([PassProjectFieldId] ASC);

