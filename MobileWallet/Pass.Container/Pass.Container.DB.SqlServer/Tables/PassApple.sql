CREATE TABLE [pscn].[PassApple] (
    [PassId]     INT            NOT NULL,
    [PassTypeId] NVARCHAR (400) NOT NULL,
    CONSTRAINT [PK_pscn.PassApple] PRIMARY KEY CLUSTERED ([PassId] ASC),
    CONSTRAINT [FK_pscn.PassApple_pscn.Pass_PassId] FOREIGN KEY ([PassId]) REFERENCES [pscn].[Pass] ([PassId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PassId]
    ON [pscn].[PassApple]([PassId] ASC);

