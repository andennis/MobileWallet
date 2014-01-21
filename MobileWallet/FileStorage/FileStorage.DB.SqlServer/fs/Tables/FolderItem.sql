CREATE TABLE [fs].[FolderItem] (
    [FolderItemId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (400) NOT NULL,
    [ParentId]     INT            NULL,
    CONSTRAINT [PK_fs.FolderItem] PRIMARY KEY CLUSTERED ([FolderItemId] ASC),
    CONSTRAINT [FK_fs.FolderItem_fs.FolderItem_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [fs].[FolderItem] ([FolderItemId])
);










GO
CREATE NONCLUSTERED INDEX [IX_ParentId]
    ON [fs].[FolderItem]([ParentId] ASC);

