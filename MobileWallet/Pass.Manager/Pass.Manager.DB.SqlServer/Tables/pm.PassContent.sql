CREATE TABLE [pm].[PassContent] (
    [PassContentId]         INT          IDENTITY (1, 1) NOT NULL,
    [SerialNumber]          VARCHAR (64) NOT NULL,
    [Status]                INT          NOT NULL,
    [PassContentTemplateId] INT          NOT NULL,
    [Version]               INT          NOT NULL,
    [CreatedDate]           DATETIME     NOT NULL,
    [UpdatedDate]           DATETIME     NOT NULL,
    [ExpDate]               DATETIME     NULL,
    [IsValid]               BIT          DEFAULT ((1)) NOT NULL,
    [AuthToken]             VARCHAR (64) DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_pm.PassContent] PRIMARY KEY CLUSTERED ([PassContentId] ASC),
    CONSTRAINT [FK_pm.PassContent_pm.PassContentTemplate_PassContentTemplateId] FOREIGN KEY ([PassContentTemplateId]) REFERENCES [pm].[PassContentTemplate] ([PassContentTemplateId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PassContentTemplateId]
    ON [pm].[PassContent]([PassContentTemplateId] ASC);

