CREATE TABLE [pm].[PassProject] (
    [PassProjectId]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (512) NOT NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [PassSiteId]        INT            NOT NULL,
    [ProjectType]       INT            NOT NULL,
    [Version]           INT            NOT NULL,
    [CreatedDate]       DATETIME       NOT NULL,
    [UpdatedDate]       DATETIME       NOT NULL,
    [PassContentId]     INT            DEFAULT ((0)) NULL,
    [PassCertificateId] INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_pm.PassProject] PRIMARY KEY CLUSTERED ([PassProjectId] ASC),
    CONSTRAINT [FK_pm.PassProject_pm.PassCertificate_PassCertificateId] FOREIGN KEY ([PassCertificateId]) REFERENCES [pm].[PassCertificate] ([PassCertificateId]),
    CONSTRAINT [FK_pm.PassProject_pm.PassSite_PassSiteId] FOREIGN KEY ([PassSiteId]) REFERENCES [pm].[PassSite] ([PassSiteId]) ON DELETE CASCADE
);








GO
CREATE NONCLUSTERED INDEX [IX_PassSiteId]
    ON [pm].[PassProject]([PassSiteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PassCertificateId]
    ON [pm].[PassProject]([PassCertificateId] ASC);

