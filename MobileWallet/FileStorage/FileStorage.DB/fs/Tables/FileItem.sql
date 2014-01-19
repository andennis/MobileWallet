CREATE TABLE [fs].[FileItem] (
    [FileItemId]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (400) NOT NULL,
    [OriginalName] NVARCHAR (400) NOT NULL,
    [FileSize]     INT            NOT NULL,
    [ParentId]     INT            NOT NULL,
    CONSTRAINT [PK_fs.FileItem] PRIMARY KEY CLUSTERED ([FileItemId] ASC),
    CONSTRAINT [FK_fs.FileItem_fs.FolderItem_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [fs].[FolderItem] ([FolderItemId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ParentId]
    ON [fs].[FileItem]([ParentId] ASC);

