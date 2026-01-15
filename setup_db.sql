IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Cars] (
    [Id] uniqueidentifier NOT NULL,
    [Make] nvarchar(max) NOT NULL,
    [Model] nvarchar(max) NOT NULL,
    [Year] int NOT NULL,
    [Price] int NOT NULL,
    [IsUsed] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Cars] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260113131303_InitialCreate', N'9.0.8');

COMMIT;
GO

