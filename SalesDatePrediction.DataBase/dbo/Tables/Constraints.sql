CREATE TABLE [dbo].[Constraints] (
    [ConstraintId]   INT            IDENTITY (1, 1) NOT NULL,
    [TableId]        INT            NOT NULL,
    [ConstraintName] NVARCHAR (128) NOT NULL,
    [ConstraintType] NVARCHAR (50)  NOT NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [UsersId]        INT            NOT NULL,
    CONSTRAINT [PK__Constrai__7396AFEE321A1BE6] PRIMARY KEY CLUSTERED ([ConstraintId] ASC),
    CONSTRAINT [FK_Constraints_Tables] FOREIGN KEY ([TableId]) REFERENCES [dbo].[Tables] ([TableId]),
    CONSTRAINT [FK_Constraints_Users] FOREIGN KEY ([UsersId]) REFERENCES [dbo].[Users] ([Id])
);

