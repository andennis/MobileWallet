CREATE TABLE [pm].[PassSiteUser] (
    [PassSiteId]     INT      NOT NULL,
    [UserId]         INT      NOT NULL,
    [Status]         INT      NOT NULL,
    [Version]        ROWVERSION      NOT NULL,
    [CreatedDate]    DATETIME NOT NULL,
    [UpdatedDate]    DATETIME NOT NULL,
    [UserState]      INT      DEFAULT ((0)) NOT NULL,
    [PassSiteUserId] INT      IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm.PassSiteUser] PRIMARY KEY CLUSTERED ([PassSiteUserId] ASC),
    CONSTRAINT [FK_pm.PassSiteUser_pm.PassSite_PassSiteId] FOREIGN KEY ([PassSiteId]) REFERENCES [pm].[PassSite] ([PassSiteId]) ON DELETE CASCADE,
    CONSTRAINT [FK_pm.PassSiteUser_pm.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [pm].[User] ([UserId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_PassSiteId]
    ON [pm].[PassSiteUser]([PassSiteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [pm].[PassSiteUser]([UserId] ASC);

