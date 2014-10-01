CREATE TABLE [pm].[PassCertificateApple] (
    [PassCertificateId] INT            NOT NULL,
    [TeamId]            NVARCHAR (128) NOT NULL,
    [PassTypeId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_pm.PassCertificateApple] PRIMARY KEY CLUSTERED ([PassCertificateId] ASC),
    CONSTRAINT [FK_pm.PassCertificateApple_pm.PassCertificate_PassCertificateId] FOREIGN KEY ([PassCertificateId]) REFERENCES [pm].[PassCertificate] ([PassCertificateId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PassCertificateId]
    ON [pm].[PassCertificateApple]([PassCertificateId] ASC);

