CREATE TABLE [dbo].[Indexes] (
    [IndexId]         INT            IDENTITY (1, 1) NOT NULL,
    [TableId]         INT            NOT NULL,
    [IndexName]       NVARCHAR (128) NOT NULL,
    [IndexType]       NVARCHAR (50)  NOT NULL,
    [IncludedColumns] NVARCHAR (MAX) NOT NULL,
    [UsersId]         INT            NOT NULL,
    CONSTRAINT [PK__Indexes__40BC8A41E16B6F69] PRIMARY KEY CLUSTERED ([IndexId] ASC),
    CONSTRAINT [FK_Indexes_Tables] FOREIGN KEY ([TableId]) REFERENCES [dbo].[Tables] ([TableId]),
    CONSTRAINT [FK_Indexes_Users] FOREIGN KEY ([UsersId]) REFERENCES [dbo].[Users] ([Id])
);

