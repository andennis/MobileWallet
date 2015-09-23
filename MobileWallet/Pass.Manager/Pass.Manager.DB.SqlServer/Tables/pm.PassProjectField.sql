CREATE TABLE [pm].[PassProjectField] (
    [PassProjectFieldId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (512) NOT NULL,
    [DefaultValue]       NVARCHAR (MAX) NULL,
    [DefaultLabel]       NVARCHAR (128) NULL,
    [PassProjectId]      INT            NOT NULL,
    [Version]            ROWVERSION     NOT NULL,
    [CreatedDate]        DATETIME       NOT NULL,
    [UpdatedDate]        DATETIME       NOT NULL,
    [Description]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_pm.PassProjectField] PRIMARY KEY CLUSTERED ([PassProjectFieldId] ASC),
    CONSTRAINT [FK_pm.PassProjectField_pm.PassProject_PassProjectId] FOREIGN KEY ([PassProjectId]) REFERENCES [pm].[PassProject] ([PassProjectId])
);






GO
CREATE NONCLUSTERED INDEX [IX_PassProjectId]
    ON [pm].[PassProjectField]([PassProjectId] ASC);

