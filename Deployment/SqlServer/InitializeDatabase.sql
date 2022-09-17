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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220917070711_InitialState')
BEGIN
    CREATE TABLE [Routes] (
        [Id] uniqueidentifier NOT NULL,
        [FullUrl] nvarchar(max) NOT NULL,
        [BestBefore] datetime2 NULL,
        CONSTRAINT [PK_Routes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220917070711_InitialState')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220917070711_InitialState', N'5.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220917070811_GetDataSchemeVersionAdded')
BEGIN
    EXEC sp_executesql N'
            IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N''GetDataSchemeVersion'' AND [type] = N''P'')
            BEGIN
                  DROP PROCEDURE dbo.GetDataSchemeVersion;
            END'
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220917070811_GetDataSchemeVersionAdded')
BEGIN
    EXEC sp_executesql N'
            CREATE PROCEDURE [dbo].[GetDataSchemeVersion]
            (@dbversion varchar(50) OUTPUT)
            AS
            BEGIN
                SET NOCOUNT ON;

    	        IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = ''dbo'' AND  TABLE_NAME = ''__EFMigrationsHistory''))
    		        select @dbversion = CONCAT(''1.0.'', CONVERT(varchar(10), (select count(*) from dbo.__EFMigrationsHistory)))
    	        else
    		        select @dbversion = ''1.0.0''
            END'
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220917070811_GetDataSchemeVersionAdded')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220917070811_GetDataSchemeVersionAdded', N'5.0.17');
END;
GO

COMMIT;
GO

