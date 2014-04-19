CREATE TABLE [pscn].[ClientDevice] (
    [ClientDeviceId] INT            IDENTITY (1, 1) NOT NULL,
    [DeviceId]       NVARCHAR (512) NOT NULL,
    CONSTRAINT [PK_pscn.ClientDevice] PRIMARY KEY CLUSTERED ([ClientDeviceId] ASC)
);



