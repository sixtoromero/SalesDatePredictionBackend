CREATE TABLE [dbo].[Columns] (
    [ColumnId]     INT            IDENTITY (1, 1) NOT NULL,
    [TableId]      INT            NOT NULL,
    [ColumnName]   NVARCHAR (128) NOT NULL,
    [DataType]     NVARCHAR (128) NOT NULL,
    [Size]         INT            NOT NULL,
    [IsNullable]   BIT            NOT NULL,
    [IsPrimaryKey] BIT            NOT NULL,
    [IsForeignKey] BIT            NOT NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [UsersId]      INT            NOT NULL,
    CONSTRAINT [PK__Columns__1AA1420F3C0FC418] PRIMARY KEY CLUSTERED ([ColumnId] ASC),
    CONSTRAINT [FK_Columns_Tables] FOREIGN KEY ([TableId]) REFERENCES [dbo].[Tables] ([TableId]),
    CONSTRAINT [FK_Columns_Users] FOREIGN KEY ([UsersId]) REFERENCES [dbo].[Users] ([Id])
);

