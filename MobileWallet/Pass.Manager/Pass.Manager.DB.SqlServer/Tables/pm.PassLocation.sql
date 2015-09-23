CREATE TABLE [pm].[PassLocation] (
    [PassLocationId]        INT            IDENTITY (1, 1) NOT NULL,
    [Altitude]              FLOAT (53)     NULL,
    [Latitude]              FLOAT (53)     NOT NULL,
    [Longitude]             FLOAT (53)     NOT NULL,
    [RelevantText]          NVARCHAR (MAX) NULL,
    [PassContentTemplateId] INT            NOT NULL,
    [Version]               ROWVERSION   NOT NULL,
    [CreatedDate]           DATETIME       NOT NULL,
    [UpdatedDate]           DATETIME       NOT NULL,
    [Name]                  NVARCHAR (512) DEFAULT ('') NOT NULL,
    [Description]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_pm.PassLocation] PRIMARY KEY CLUSTERED ([PassLocationId] ASC),
    CONSTRAINT [FK_pm.PassLocation_pm.PassContentTemplate_PassContentTemplateId] FOREIGN KEY ([PassContentTemplateId]) REFERENCES [pm].[PassContentTemplate] ([PassContentTemplateId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_PassContentTemplateId]
    ON [pm].[PassLocation]([PassContentTemplateId] ASC);

