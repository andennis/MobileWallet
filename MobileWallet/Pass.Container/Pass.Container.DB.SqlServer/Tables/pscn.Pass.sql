CREATE TABLE [pscn].[Pass] (
    [PassId]         INT            IDENTITY (1, 1) NOT NULL,
    [AuthToken]      NVARCHAR (400) NOT NULL,
    [SerialNumber]   NVARCHAR (400) NOT NULL,
    [ExpirationDate] DATETIME       NULL,
    [Status]         INT            NOT NULL,
    [Version]        INT            NOT NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [UpdatedDate]    DATETIME       NOT NULL,
    [PassTemplateId] INT            NOT NULL,
    CONSTRAINT [PK_pscn.Pass] PRIMARY KEY CLUSTERED ([PassId] ASC),
    CONSTRAINT [FK_pscn.Pass_pscn.PassTemplate_PassTemplateId] FOREIGN KEY ([PassTemplateId]) REFERENCES [pscn].[PassTemplate] ([PassTemplateId])
);




GO
CREATE NONCLUSTERED INDEX [IX_PassTemplateId]
    ON [pscn].[Pass]([PassTemplateId] ASC);

