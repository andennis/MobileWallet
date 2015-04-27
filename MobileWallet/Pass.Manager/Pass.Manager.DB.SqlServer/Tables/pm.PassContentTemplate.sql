CREATE TABLE [pm].[PassContentTemplate] (
    [PassContentTemplateId]   INT            IDENTITY (1, 1) NOT NULL,
    [Description]             NVARCHAR (MAX) NOT NULL,
    [OrganizationName]        NVARCHAR (512) NOT NULL,
    [PassStyle]               INT            NOT NULL,
    [MaxDistance]             INT            NULL,
    [RelevantDate]            DATETIME       NULL,
    [GroupingIdentifier]      NVARCHAR (MAX) NULL,
    [LogoText]                NVARCHAR (MAX) NULL,
    [SuppressStripShine]      BIT            NOT NULL,
    [Version]                 INT            NOT NULL,
    [CreatedDate]             DATETIME       NOT NULL,
    [UpdatedDate]             DATETIME       NOT NULL,
    [PassProjectId]           INT            DEFAULT ((0)) NOT NULL,
    [Name]                    NVARCHAR (512) DEFAULT ('') NOT NULL,
    [TransitType]             INT            NULL,
    [IsDefault]               BIT            DEFAULT ((0)) NOT NULL,
    [Status]                  INT            DEFAULT ((1)) NOT NULL,
    [PassContainerTemplateId] INT            NULL,
    CONSTRAINT [PK_pm.PassContentTemplate] PRIMARY KEY CLUSTERED ([PassContentTemplateId] ASC),
    CONSTRAINT [FK_pm.PassContentTemplate_pm.PassProject_PassProjectId] FOREIGN KEY ([PassProjectId]) REFERENCES [pm].[PassProject] ([PassProjectId])
);




GO
CREATE NONCLUSTERED INDEX [IX_PassProjectId]
    ON [pm].[PassContentTemplate]([PassProjectId] ASC);

