CREATE TABLE [pscn].[SequenceCounter]
(
	[SequenceCounterId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Counter] INT NOT NULL DEFAULT 0
)

GO

CREATE UNIQUE INDEX [IX_SequenceCounter_Name] ON [pscn].[SequenceCounter] ([Name])
