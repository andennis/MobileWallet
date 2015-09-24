CREATE TABLE [pm].[PassContentField] (
    [PassContentFieldId] INT            IDENTITY (1, 1) NOT NULL,
    [PassProjectFieldId] INT            NOT NULL,
    [FieldLabel]         NVARCHAR (512) NULL,
    [FieldValue]         NVARCHAR (512) NULL,
    [Version]            INT            NOT NULL,
    [CreatedDate]        DATETIME       NOT NULL,
    [UpdatedDate]        DATETIME       NOT NULL,
    [PassContentId]      INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_pm.PassContentField] PRIMARY KEY CLUSTERED ([PassContentFieldId] ASC),
    CONSTRAINT [FK_pm.PassContentField_pm.PassContent_PassContentId] FOREIGN KEY ([PassContentId]) REFERENCES [pm].[PassContent] ([PassContentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_pm.PassContentField_pm.PassProjectField_PassProjectFieldId] FOREIGN KEY ([PassProjectFieldId]) REFERENCES [pm].[PassProjectField] ([PassProjectFieldId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PassContentId]
    ON [pm].[PassContentField]([PassContentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PassProjectFieldId]
    ON [pm].[PassContentField]([PassProjectFieldId] ASC);

