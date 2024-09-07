CREATE TABLE [dbo].[Tables] (
    [TableId]     INT            IDENTITY (1, 1) NOT NULL,
    [DatabaseId]  INT            NOT NULL,
    [Scheme]      NVARCHAR (20)  NOT NULL,
    [TableName]   NVARCHAR (128) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Status]      BIT            NOT NULL,
    [UsersId]     INT            NOT NULL,
    CONSTRAINT [PK__Tables__7D5F01EE34B21E11] PRIMARY KEY CLUSTERED ([TableId] ASC),
    CONSTRAINT [FK_Tables_Databases] FOREIGN KEY ([DatabaseId]) REFERENCES [dbo].[Databases] ([DatabaseId]),
    CONSTRAINT [FK_Tables_Users] FOREIGN KEY ([UsersId]) REFERENCES [dbo].[Users] ([Id])
);

