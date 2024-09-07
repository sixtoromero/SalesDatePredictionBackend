CREATE TABLE [dbo].[Databases] (
    [DatabaseId]       INT            IDENTITY (1, 1) NOT NULL,
    [DatabaseName]     NVARCHAR (128) NOT NULL,
    [Description]      NVARCHAR (MAX) NULL,
    [ConnectionString] NVARCHAR (MAX) NULL,
    [UsersId]          INT            NOT NULL,
    CONSTRAINT [PK__Database__2C9BE46F58D5EE13] PRIMARY KEY CLUSTERED ([DatabaseId] ASC),
    CONSTRAINT [FK_Databases_Users] FOREIGN KEY ([UsersId]) REFERENCES [dbo].[Users] ([Id])
);

