CREATE TABLE [pm].[PassCertificate] (
    [PassCertificateId]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (512) NOT NULL,
    [Description]          NVARCHAR (MAX) NULL,
    [ExpDate]              DATETIME       NOT NULL,
    [CertificateStorageId] INT            NOT NULL,
    [Version]              INT            NOT NULL,
    [CreatedDate]          DATETIME       NOT NULL,
    [UpdatedDate]          DATETIME       NOT NULL,
    [CertificateFileName]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_pm.PassCertificate] PRIMARY KEY CLUSTERED ([PassCertificateId] ASC)
);



