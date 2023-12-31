/*
CREATE SCHEMA [fs]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[fs].[GetFolderPath]') AND type in (N'P', N'PC'))
--DROP PROCEDURE [fs].[GetFolderPath]
--GO

-- =============================================
-- Author:		Denis Andreev
-- Create date: 01/25/2014
-- Description:	
-- =============================================
CREATE PROCEDURE [fs].[GetFolderPath]
	@FolderItemId INT
AS
BEGIN
	;WITH folders(FolderItemId, ParentId, FolderPath) AS
	(
		SELECT FolderItemId, ParentId, CAST(Name as NVARCHAR(MAX)) as FolderPath
		FROM fs.FolderItem WHERE FolderItemId = @FolderItemId
		UNION ALL
		SELECT f2.FolderItemId, f2.ParentId, f2.Name + '\' + f1.FolderPath
		FROM folders f1
		INNER JOIN fs.FolderItem f2 ON f1.ParentId = f2.FolderItemId
	)
    SELECT f1.FolderPath FROM folders f1
    WHERE f1.ParentId IS NULL
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Denis Andreev
-- Create date: 01/21/2014
-- Description:	
-- =============================================
CREATE PROCEDURE [fs].[GetFreeFolder]
	@FolderLevel INT,
	@MaxItemsNumber INT
AS
BEGIN
    DECLARE @FolderItemId INT = NULL;

	;WITH folders(FolderItemId, ParentId, FolderLevel) AS
	(
		SELECT FolderItemId, ParentId, 0 AS FolderLevel
		FROM fs.FolderItem WHERE ParentId IS NULL
		UNION ALL
		SELECT f2.FolderItemId, f2.ParentId, f1.FolderLevel + 1 
		FROM folders f1
		INNER JOIN fs.FolderItem f2 on f1.FolderItemId = f2.ParentId
	)
    SELECT TOP 1 @FolderItemId = t.FolderItemId FROM 
    (
        SELECT f.FolderItemId, COUNT(si.ParentId) AS Number FROM folders f
        LEFT JOIN fs.StorageItem si ON si.ParentId = f.FolderItemId
        WHERE f.FolderLevel = @FolderLevel
        GROUP BY f.FolderItemId
        UNION ALL
        SELECT f1.FolderItemId, COUNT(f2.ParentId) AS  Number FROM folders f1
        LEFT JOIN fs.FolderItem f2 ON f2.ParentId = f1.FolderItemId
        WHERE f1.FolderLevel = @FolderLevel
        GROUP BY f1.FolderItemId
    )t
    GROUP BY t.FolderItemId
    HAVING SUM(t.Number) < @MaxItemsNumber
    ORDER BY SUM(t.Number)

    SELECT 
        FolderItemId, 
        ParentId, 
        Name
    FROM fs.FolderItem 
    WHERE FolderItemId = @FolderItemId
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Denis Andreev
-- Create date: 01/27/2014
-- Description:	
-- =============================================
CREATE PROCEDURE [fs].[GetStorageItemPath]
	@StorageItemId INT
AS
BEGIN
	;WITH folders(FolderItemId, ParentId, FilePath) AS
	(
		SELECT fi.FolderItemId, fi.ParentId, CAST(fi.Name + '\' + si.Name AS NVARCHAR(MAX)) AS FilePath
		FROM fs.FolderItem fi
        INNER JOIN fs.StorageItem si ON si.ParentId = fi.FolderItemId AND si.[Status] = 1/*Active*/
        WHERE si.StorageItemId = @StorageItemId 
		UNION ALL
		SELECT f2.FolderItemId, f2.ParentId, f2.Name + '\' + f1.FilePath
		FROM folders f1
		INNER JOIN fs.FolderItem f2 ON f1.ParentId = f2.FolderItemId
	)
    SELECT f1.FilePath FROM folders f1
    WHERE f1.ParentId IS NULL
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [fs].[FolderItem](
	[FolderItemId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](400) NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_fs.FolderItem] PRIMARY KEY CLUSTERED 
(
	[FolderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [fs].[StorageItem](
	[StorageItemId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](400) NOT NULL,
	[OriginalName] [nvarchar](400) NULL,
	[Size] [bigint] NULL,
	[Status] [int] NOT NULL,
	[ItemType] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
 CONSTRAINT [PK_fs.StorageItem] PRIMARY KEY CLUSTERED 
(
	[StorageItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [fs].[FolderItem]  WITH CHECK ADD  CONSTRAINT [FK_fs.FolderItem_fs.FolderItem_ParentId] FOREIGN KEY([ParentId])
REFERENCES [fs].[FolderItem] ([FolderItemId])
GO
ALTER TABLE [fs].[FolderItem] CHECK CONSTRAINT [FK_fs.FolderItem_fs.FolderItem_ParentId]
GO
ALTER TABLE [fs].[StorageItem]  WITH CHECK ADD  CONSTRAINT [FK_fs.StorageItem_fs.FolderItem_ParentId] FOREIGN KEY([ParentId])
REFERENCES [fs].[FolderItem] ([FolderItemId])
ON DELETE CASCADE
GO
ALTER TABLE [fs].[StorageItem] CHECK CONSTRAINT [FK_fs.StorageItem_fs.FolderItem_ParentId]
GO
*/