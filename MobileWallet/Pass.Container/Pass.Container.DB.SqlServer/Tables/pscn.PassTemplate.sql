CREATE TABLE [pscn].[PassTemplate] (
    [PassTemplateId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (512) NOT NULL,
    [PackageId]      INT            NOT NULL,
    [Status]         INT            NOT NULL,
    [Version]        ROWVERSION     NOT NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [UpdatedDate]    DATETIME       NOT NULL,
    CONSTRAINT [PK_pscn.PassTemplate] PRIMARY KEY CLUSTERED ([PassTemplateId] ASC)
);



