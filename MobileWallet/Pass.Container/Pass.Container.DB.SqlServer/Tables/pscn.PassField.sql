CREATE TABLE [pscn].[PassField] (
    [PassFieldId]    INT            IDENTITY (1, 1) NOT NULL,
    [PassTemplateId] INT            NOT NULL,
    [Name]           NVARCHAR (400) NOT NULL,
    [Version]        INT            NOT NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [UpdatedDate]    DATETIME       NOT NULL,
    CONSTRAINT [PK_pscn.PassField] PRIMARY KEY CLUSTERED ([PassFieldId] ASC),
    CONSTRAINT [FK_pscn.PassField_pscn.PassTemplate_PassTemplateId] FOREIGN KEY ([PassTemplateId]) REFERENCES [pscn].[PassTemplate] ([PassTemplateId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_PassTemplateId]
    ON [pscn].[PassField]([PassTemplateId] ASC);

