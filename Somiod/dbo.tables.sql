CREATE TABLE [dbo].[application] (
    [id]                INT           IDENTITY (1, 1) NOT NULL,
    [name]              NVARCHAR (50) NOT NULL,
    [creation_datetime] DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC)
);

CREATE TABLE [dbo].[container] (
    [id]                INT           IDENTITY (1, 1) NOT NULL,
    [name]              NVARCHAR (50) NOT NULL,
    [creation_datetime] DATETIME2 (7) NULL,
    [parent]            INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC),
    CONSTRAINT [FK_Container_Application] FOREIGN KEY ([parent]) REFERENCES [dbo].[application] ([id])
);

CREATE TABLE [dbo].[record] (
    [id]                INT             IDENTITY (1, 1) NOT NULL,
    [name]              NVARCHAR (50)   NOT NULL,
    [content]           NVARCHAR (4000) NULL,
    [creation_datetime] DATETIME2 (7)   NULL,
    [parent]            INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC),
    CONSTRAINT [FK_Record_Container] FOREIGN KEY ([parent]) REFERENCES [dbo].[container] ([id])
);

CREATE TABLE [dbo].[notification] (
    [id]                INT            IDENTITY (1, 1) NOT NULL,
    [name]              NVARCHAR (50)  NOT NULL,
    [creation_datetime] DATETIME2 (7)  NULL,
    [parent]            INT            NOT NULL,
    [event]             TINYINT        NOT NULL,
    [endpoint]          NVARCHAR (500) NULL,
    [enabled]           BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC),
    CONSTRAINT [FK_Notification_Container] FOREIGN KEY ([parent]) REFERENCES [dbo].[container] ([id]),
    CHECK ([event]=(3) OR [event]=(2) OR [event]=(1))
);



