CREATE TABLE [pscn].[PassFieldValue] (
    [PassFieldValueId] INT            IDENTITY (1, 1) NOT NULL,
    [PassFieldId]      INT            NOT NULL,
    [PassId]           INT            NOT NULL,
    [Label]            NVARCHAR (512) NULL,
    [Value]            NVARCHAR (512) NULL,
    [Version]          INT            NOT NULL,
    [CreatedDate]      DATETIME       NOT NULL,
    [UpdatedDate]      DATETIME       NOT NULL,
    CONSTRAINT [PK_pscn.PassFieldValue] PRIMARY KEY CLUSTERED ([PassFieldValueId] ASC),
    CONSTRAINT [FK_pscn.PassFieldValue_pscn.Pass_PassId] FOREIGN KEY ([PassId]) REFERENCES [pscn].[Pass] ([PassId]) ON DELETE CASCADE,
    CONSTRAINT [FK_pscn.PassFieldValue_pscn.PassField_PassFieldId] FOREIGN KEY ([PassFieldId]) REFERENCES [pscn].[PassField] ([PassFieldId])
);






GO
CREATE NONCLUSTERED INDEX [IX_PassId]
    ON [pscn].[PassFieldValue]([PassId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PassFieldId]
    ON [pscn].[PassFieldValue]([PassFieldId] ASC);

