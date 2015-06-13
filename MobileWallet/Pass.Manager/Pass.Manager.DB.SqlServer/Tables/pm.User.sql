CREATE TABLE [pm].[User] (
    [UserId]      INT            IDENTITY (1, 1) NOT NULL,
    [UserName]    NVARCHAR (512) NOT NULL,
    [FirstName]   NVARCHAR (512) NULL,
    [LastName]    NVARCHAR (512) NULL,
    [Password]    NVARCHAR (512) NULL,
    [Version]     INT            NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [UpdatedDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_pm.User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Name]
    ON [pm].[User]([UserName] ASC);

