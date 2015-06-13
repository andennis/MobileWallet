CREATE TABLE [pscn].[ClientDevice] (
    [ClientDeviceId] INT          IDENTITY (1, 1) NOT NULL,
    [DeviceId]       VARCHAR (64) NOT NULL,
    [DeviceType]     INT          NOT NULL,
    [Version]        INT          NOT NULL,
    [CreatedDate]    DATETIME     NOT NULL,
    [UpdatedDate]    DATETIME     NOT NULL,
    CONSTRAINT [PK_pscn.ClientDevice] PRIMARY KEY CLUSTERED ([ClientDeviceId] ASC)
);









