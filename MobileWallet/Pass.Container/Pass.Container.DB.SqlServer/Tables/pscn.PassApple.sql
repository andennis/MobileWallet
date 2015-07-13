CREATE TABLE [pscn].[PassApple] (
    [PassNativeId] INT           NOT NULL,
    [PassTypeId]   VARCHAR (128) NOT NULL,
    CONSTRAINT [PK_pscn.PassApple] PRIMARY KEY CLUSTERED ([PassNativeId] ASC),
    CONSTRAINT [FK_pscn.PassApple_pscn.PassNative_PassNativeId] FOREIGN KEY ([PassNativeId]) REFERENCES [pscn].[PassNative] ([PassNativeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PassNativeId]
    ON [pscn].[PassApple]([PassNativeId] ASC);

