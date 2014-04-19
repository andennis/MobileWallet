CREATE TABLE [cer].[Certificate] (
    [CertificateId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (512) NOT NULL,
    [Password]      NVARCHAR (512) NULL,
    [FileId]        INT            NOT NULL,
    [Status]        INT            NOT NULL,
    [Version]       INT            NOT NULL,
    [CreatedDate]   DATETIME       NOT NULL,
    [UpdatedDate]   DATETIME       NOT NULL,
    CONSTRAINT [PK_cer.Certificate] PRIMARY KEY CLUSTERED ([CertificateId] ASC)
);

