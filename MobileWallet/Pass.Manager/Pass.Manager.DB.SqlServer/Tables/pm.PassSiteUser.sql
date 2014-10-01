CREATE TABLE [pm].[PassSiteUser] (
    [PassSiteId]  INT      NOT NULL,
    [UserId]      INT      NOT NULL,
    [Status]      INT      NOT NULL,
    [Version]     INT      NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    [UpdatedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_pm.PassSiteUser] PRIMARY KEY CLUSTERED ([PassSiteId] ASC, [UserId] ASC),
    CONSTRAINT [FK_pm.PassSiteUser_pm.PassSite_PassSiteId] FOREIGN KEY ([PassSiteId]) REFERENCES [pm].[PassSite] ([PassSiteId]) ON DELETE CASCADE,
    CONSTRAINT [FK_pm.PassSiteUser_pm.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [pm].[User] ([UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PassSiteId]
    ON [pm].[PassSiteUser]([PassSiteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [pm].[PassSiteUser]([UserId] ASC);

