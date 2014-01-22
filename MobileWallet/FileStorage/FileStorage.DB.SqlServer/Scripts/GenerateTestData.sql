DELETE FROM fs.StorageItem
DELETE FROM fs.FolderItem

INSERT INTO fs.FolderItem (Name, ParentId)
VALUES (N'Folder1', NULL)
GO
INSERT INTO fs.FolderItem (Name, ParentId)
VALUES (N'Folder11', @@IDENTITY)
GO
DECLARE @ParentId INT = @@IDENTITY
INSERT INTO fs.FolderItem (Name, ParentId)
VALUES (N'Folder111', @ParentId)
INSERT INTO fs.FolderItem (Name, ParentId)
VALUES (N'Folder112', @ParentId)
GO
DECLARE @ParentId INT = @@IDENTITY
INSERT INTO fs.StorageItem (Name, OriginalName, Size, [Status], ItemType, ParentId)
VALUES (N'File111_1', N'File1', 0, 0, 0, @ParentId) 
INSERT INTO fs.StorageItem (Name, OriginalName, Size, [Status], ItemType, ParentId)
VALUES (N'File111_2', N'File1', 0, 0, 0, @ParentId) 
GO
DECLARE @ParentId INT = (SELECT FolderItemId FROM fs.FolderItem WHERE Name = 'Folder111')
INSERT INTO fs.FolderItem (Name, ParentId)
VALUES (N'Folder111_1', @ParentId)
GO


INSERT INTO fs.FolderItem (Name, ParentId)
VALUES (N'Folder2', NULL)
GO
INSERT INTO fs.FolderItem (Name, ParentId)
VALUES (N'Folder21', @@IDENTITY)
GO
INSERT INTO fs.FolderItem (Name, ParentId)
VALUES (N'Folder211', @@IDENTITY)
GO
DECLARE @ParentId INT = @@IDENTITY
INSERT INTO fs.StorageItem (Name, OriginalName, Size, [Status], ItemType, ParentId)
VALUES (N'File211_1', N'File211_1', 0, 0, 0, @ParentId) 
INSERT INTO fs.StorageItem (Name, OriginalName, Size, [Status], ItemType, ParentId)
VALUES (N'File211_2', N'File211_2', 0, 0, 0, @ParentId) 
INSERT INTO fs.StorageItem (Name, OriginalName, Size, [Status], ItemType, ParentId)
VALUES (N'File211_3', N'File211_3', 0, 0, 0, @ParentId) 
GO
