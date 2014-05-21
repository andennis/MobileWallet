CREATE TABLE [pscn].[ClientDeviceApple] (
    [ClientDeviceId] INT           NOT NULL,
    [PushToken]      NVARCHAR (64) NOT NULL,
    CONSTRAINT [PK_pscn.ClientDeviceApple] PRIMARY KEY CLUSTERED ([ClientDeviceId] ASC),
    CONSTRAINT [FK_pscn.ClientDeviceApple_pscn.ClientDevice_ClientDeviceId] FOREIGN KEY ([ClientDeviceId]) REFERENCES [pscn].[ClientDevice] ([ClientDeviceId])
);






GO
CREATE NONCLUSTERED INDEX [IX_ClientDeviceId]
    ON [pscn].[ClientDeviceApple]([ClientDeviceId] ASC);

