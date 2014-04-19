CREATE TABLE [fs].[StorageItem] (
    [StorageItemId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (512) NOT NULL,
    [OriginalName]  NVARCHAR (512) NULL,
    [Size]          BIGINT         NULL,
    [Status]        INT            NOT NULL,
    [ItemType]      INT            NOT NULL,
    [Version]       INT            NOT NULL,
    [CreatedDate]   DATETIME       NOT NULL,
    [UpdatedDate]   DATETIME       NOT NULL,
    [ParentId]      INT            NOT NULL,
    CONSTRAINT [PK_fs.StorageItem] PRIMARY KEY CLUSTERED ([StorageItemId] ASC),
    CONSTRAINT [FK_fs.StorageItem_fs.FolderItem_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [fs].[FolderItem] ([FolderItemId]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_ParentId]
    ON [fs].[StorageItem]([ParentId] ASC);

