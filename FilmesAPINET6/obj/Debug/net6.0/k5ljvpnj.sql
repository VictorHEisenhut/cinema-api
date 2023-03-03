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
GO

CREATE TABLE [Filmes] (
    [ID] int NOT NULL IDENTITY,
    [Titulo] nvarchar(70) NOT NULL,
    [Genero] nvarchar(70) NOT NULL,
    [Duracao] int NOT NULL,
    CONSTRAINT [PK_Filmes] PRIMARY KEY ([ID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230224174058_CriandoTabelaDeFilme', N'7.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Enderecos] (
    [ID] int NOT NULL IDENTITY,
    [Logradouro] nvarchar(max) NOT NULL,
    [Numero] int NOT NULL,
    CONSTRAINT [PK_Enderecos] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Cinemas] (
    [ID] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [EnderecoId] int NOT NULL,
    CONSTRAINT [PK_Cinemas] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Cinemas_Enderecos_EnderecoId] FOREIGN KEY ([EnderecoId]) REFERENCES [Enderecos] ([ID]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Cinemas_EnderecoId] ON [Cinemas] ([EnderecoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230228172700_Cinema e Endereço', N'7.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Sessoes] (
    [ID] int NOT NULL IDENTITY,
    [FilmeID] int NOT NULL,
    CONSTRAINT [PK_Sessoes] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Sessoes_Filmes_FilmeID] FOREIGN KEY ([FilmeID]) REFERENCES [Filmes] ([ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Sessoes_FilmeID] ON [Sessoes] ([FilmeID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230228183159_Sessao e filme', N'7.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Sessoes] ADD [CinemaID] int NULL;
GO

CREATE INDEX [IX_Sessoes_CinemaID] ON [Sessoes] ([CinemaID]);
GO

ALTER TABLE [Sessoes] ADD CONSTRAINT [FK_Sessoes_Cinemas_CinemaID] FOREIGN KEY ([CinemaID]) REFERENCES [Cinemas] ([ID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230228185113_Sessao e cinema', N'7.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Sessoes] DROP CONSTRAINT [FK_Sessoes_Filmes_FilmeID];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sessoes]') AND [c].[name] = N'FilmeID');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Sessoes] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Sessoes] ALTER COLUMN [FilmeID] int NULL;
GO

ALTER TABLE [Sessoes] ADD CONSTRAINT [FK_Sessoes_Filmes_FilmeID] FOREIGN KEY ([FilmeID]) REFERENCES [Filmes] ([ID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230301172712_FilmeID nulo', N'7.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Sessoes] DROP CONSTRAINT [FK_Sessoes_Cinemas_CinemaID];
GO

ALTER TABLE [Sessoes] DROP CONSTRAINT [FK_Sessoes_Filmes_FilmeID];
GO

ALTER TABLE [Sessoes] DROP CONSTRAINT [PK_Sessoes];
GO

DROP INDEX [IX_Sessoes_FilmeID] ON [Sessoes];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sessoes]') AND [c].[name] = N'ID');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Sessoes] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Sessoes] DROP COLUMN [ID];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sessoes]') AND [c].[name] = N'FilmeID');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Sessoes] DROP CONSTRAINT [' + @var2 + '];');
UPDATE [Sessoes] SET [FilmeID] = 0 WHERE [FilmeID] IS NULL;
ALTER TABLE [Sessoes] ALTER COLUMN [FilmeID] int NOT NULL;
ALTER TABLE [Sessoes] ADD DEFAULT 0 FOR [FilmeID];
GO

DROP INDEX [IX_Sessoes_CinemaID] ON [Sessoes];
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sessoes]') AND [c].[name] = N'CinemaID');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Sessoes] DROP CONSTRAINT [' + @var3 + '];');
UPDATE [Sessoes] SET [CinemaID] = 0 WHERE [CinemaID] IS NULL;
ALTER TABLE [Sessoes] ALTER COLUMN [CinemaID] int NOT NULL;
ALTER TABLE [Sessoes] ADD DEFAULT 0 FOR [CinemaID];
CREATE INDEX [IX_Sessoes_CinemaID] ON [Sessoes] ([CinemaID]);
GO

ALTER TABLE [Sessoes] ADD CONSTRAINT [PK_Sessoes] PRIMARY KEY ([FilmeID], [CinemaID]);
GO

ALTER TABLE [Sessoes] ADD CONSTRAINT [FK_Sessoes_Cinemas_CinemaID] FOREIGN KEY ([CinemaID]) REFERENCES [Cinemas] ([ID]) ON DELETE CASCADE;
GO

ALTER TABLE [Sessoes] ADD CONSTRAINT [FK_Sessoes_Filmes_FilmeID] FOREIGN KEY ([FilmeID]) REFERENCES [Filmes] ([ID]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230301180935_Cinema e filme', N'7.0.3');
GO

COMMIT;
GO

