CREATE TABLE [pm].[PassSite] (
    [PassSiteId]  INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (512) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Version]     INT            NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [UpdatedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_pm.PassSite] PRIMARY KEY CLUSTERED ([PassSiteId] ASC)
);

