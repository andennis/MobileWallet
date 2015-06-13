CREATE TABLE [pscn].[Registration] (
    [ClientDeviceId] INT      NOT NULL,
    [PassId]         INT      NOT NULL,
    [Status]         INT      NOT NULL,
    [Version]        INT      NOT NULL,
    [CreatedDate]    DATETIME NOT NULL,
    [UpdatedDate]    DATETIME NOT NULL,
    [UnregisterDate] DATETIME NULL,
    CONSTRAINT [PK_pscn.Registration] PRIMARY KEY CLUSTERED ([ClientDeviceId] ASC, [PassId] ASC),
    CONSTRAINT [FK_pscn.Registration_pscn.ClientDevice_ClientDeviceId] FOREIGN KEY ([ClientDeviceId]) REFERENCES [pscn].[ClientDevice] ([ClientDeviceId]) ON DELETE CASCADE,
    CONSTRAINT [FK_pscn.Registration_pscn.Pass_PassId] FOREIGN KEY ([PassId]) REFERENCES [pscn].[Pass] ([PassId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_ClientDeviceId]
    ON [pscn].[Registration]([ClientDeviceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PassId]
    ON [pscn].[Registration]([PassId] ASC);

