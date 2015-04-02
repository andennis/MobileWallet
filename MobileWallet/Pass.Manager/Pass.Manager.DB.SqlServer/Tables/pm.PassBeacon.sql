CREATE TABLE [pm].[PassBeacon] (
    [PassBeaconId]          INT            IDENTITY (1, 1) NOT NULL,
    [ProximityUuid]         NVARCHAR (128) NOT NULL,
    [RelevantText]          NVARCHAR (MAX) NULL,
    [PassContentTemplateId] INT            NOT NULL,
    [Version]               INT            NOT NULL,
    [CreatedDate]           DATETIME       NOT NULL,
    [UpdatedDate]           DATETIME       NOT NULL,
    CONSTRAINT [PK_pm.PassBeacon] PRIMARY KEY CLUSTERED ([PassBeaconId] ASC),
    CONSTRAINT [FK_pm.PassBeacon_pm.PassContentTemplate_PassContentTemplateId] FOREIGN KEY ([PassContentTemplateId]) REFERENCES [pm].[PassContentTemplate] ([PassContentTemplateId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PassContentTemplateId]
    ON [pm].[PassBeacon]([PassContentTemplateId] ASC);

