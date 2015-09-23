CREATE TABLE [pm].[PassSiteCertificate] (
    [PassSiteId]            INT      NOT NULL,
    [PassCertificateId]     INT      NOT NULL,
    [Version]               ROWVERSION      NOT NULL,
    [CreatedDate]           DATETIME NOT NULL,
    [UpdatedDate]           DATETIME NOT NULL,
    [PassSiteCertificateId] INT      IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm.PassSiteCertificate] PRIMARY KEY CLUSTERED ([PassSiteCertificateId] ASC),
    CONSTRAINT [FK_pm.PassSiteCertificate_pm.PassCertificate_PassCertificateId] FOREIGN KEY ([PassCertificateId]) REFERENCES [pm].[PassCertificate] ([PassCertificateId]) ON DELETE CASCADE,
    CONSTRAINT [FK_pm.PassSiteCertificate_pm.PassSite_PassSiteId] FOREIGN KEY ([PassSiteId]) REFERENCES [pm].[PassSite] ([PassSiteId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_PassSiteId]
    ON [pm].[PassSiteCertificate]([PassSiteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PassCertificateId]
    ON [pm].[PassSiteCertificate]([PassCertificateId] ASC);

