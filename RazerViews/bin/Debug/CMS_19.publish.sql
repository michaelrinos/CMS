﻿/*
Deployment script for CMS

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "CMS"
:setvar DefaultFilePrefix "CMS"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[CSSContent]...';


GO
CREATE TABLE [dbo].[CSSContent] (
    [CSSContentId] INT                IDENTITY (1, 1) NOT NULL,
    [CSSContent]   NVARCHAR (MAX)     NOT NULL,
    [InsertBy]     NVARCHAR (50)      NOT NULL,
    [LastModified] DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([CSSContentId] ASC)
);


GO
PRINT N'Creating [dbo].[CSSContent_History]...';


GO
CREATE TABLE [dbo].[CSSContent_History] (
    [HistoryId]        INT                IDENTITY (1, 1) NOT NULL,
    [HistoryTimestamp] DATETIME           NOT NULL,
    [CSSContentId]     INT                NOT NULL,
    [CSSContent]       NVARCHAR (MAX)     NOT NULL,
    [InsertBy]         NVARCHAR (50)      NOT NULL,
    [LastModified]     DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([HistoryId] ASC)
);


GO
PRINT N'Creating [dbo].[HTMLContent]...';


GO
CREATE TABLE [dbo].[HTMLContent] (
    [HTMLContentId] INT                IDENTITY (1, 1) NOT NULL,
    [HTMLContent]   NVARCHAR (MAX)     NOT NULL,
    [InsertBy]      NVARCHAR (50)      NOT NULL,
    [LastModified]  DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([HTMLContentId] ASC)
);


GO
PRINT N'Creating [dbo].[HTMLContent_History]...';


GO
CREATE TABLE [dbo].[HTMLContent_History] (
    [HistoryId]        INT                IDENTITY (1, 1) NOT NULL,
    [HistoryTimestamp] DATETIME           NOT NULL,
    [HTMLContentId]    INT                NOT NULL,
    [HTMLContent]      NVARCHAR (MAX)     NOT NULL,
    [InsertBy]         NVARCHAR (50)      NOT NULL,
    [LastModified]     DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([HistoryId] ASC)
);


GO
PRINT N'Creating [dbo].[JSContent]...';


GO
CREATE TABLE [dbo].[JSContent] (
    [JSContentId]  INT                IDENTITY (1, 1) NOT NULL,
    [JSContent]    NVARCHAR (MAX)     NOT NULL,
    [InsertBy]     NVARCHAR (50)      NOT NULL,
    [LastModified] DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([JSContentId] ASC)
);


GO
PRINT N'Creating [dbo].[JSContent_History]...';


GO
CREATE TABLE [dbo].[JSContent_History] (
    [HistoryId]        INT                IDENTITY (1, 1) NOT NULL,
    [HistoryTimestamp] DATETIME           NOT NULL,
    [JSContentId]      INT                NOT NULL,
    [JSContent]        NVARCHAR (MAX)     NOT NULL,
    [InsertBy]         NVARCHAR (50)      NOT NULL,
    [LastModified]     DATETIMEOFFSET (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([HistoryId] ASC)
);


GO
PRINT N'Creating [dbo].[RazerViews]...';


GO
CREATE TABLE [dbo].[RazerViews] (
    [RazerViewId]   INT                IDENTITY (1, 1) NOT NULL,
    [Location]      NVARCHAR (MAX)     NOT NULL,
    [CSSContentId]  INT                NOT NULL,
    [HTMLContentId] INT                NOT NULL,
    [JSContentId]   INT                NOT NULL,
    [LastModified]  DATETIMEOFFSET (7) NOT NULL,
    [LastRequested] DATETIME2 (7)      NULL,
    [Model]         NVARCHAR (100)     NULL,
    PRIMARY KEY CLUSTERED ([RazerViewId] ASC)
);


GO
PRINT N'Creating [dbo].[FK_CSSContentId_To_CSSContent]...';


GO
ALTER TABLE [dbo].[RazerViews]
    ADD CONSTRAINT [FK_CSSContentId_To_CSSContent] FOREIGN KEY ([CSSContentId]) REFERENCES [dbo].[CSSContent] ([CSSContentId]);


GO
PRINT N'Creating [dbo].[FK_HTMLContentId_To_CSSContent]...';


GO
ALTER TABLE [dbo].[RazerViews]
    ADD CONSTRAINT [FK_HTMLContentId_To_CSSContent] FOREIGN KEY ([HTMLContentId]) REFERENCES [dbo].[HTMLContent] ([HTMLContentId]);


GO
PRINT N'Creating [dbo].[FK_JSContentId_To_CSSContdent]...';


GO
ALTER TABLE [dbo].[RazerViews]
    ADD CONSTRAINT [FK_JSContentId_To_CSSContdent] FOREIGN KEY ([JSContentId]) REFERENCES [dbo].[JSContent] ([JSContentId]);


GO
PRINT N'Creating [dbo].[CreateRazerView]...';


GO
CREATE PROCEDURE [dbo].[CreateRazerView]
	@RazerViewId	int,
	@JSContentId	int,
	@CSSContentId	int,
	@HTMLContentId	int,
	@JSContent		NVARCHAR(MAX),
	@CSSContent		NVARCHAR(MAX),	
	@HTMLContent	NVARCHAR(MAX),
	@Location		NVARCHAR(MAX),
	@InsertBy		NVARCHAR(50),
	@LastModified	DateTimeOffset

AS
	declare @likeLocation nvarchar(max) = '%' + @Location + '%';
	IF NOT EXISTS (SELECT * FROM [dbo].[RazerViews]
                   WHERE RazerViewId = @RazerViewId or 
				   Location = @Location				or 
				   Location like @likeLocation)
   BEGIN

	INSERT INTO [dbo].[JSContent] (JSContent, InsertBy, LastModified)
		VALUES (@JSContent, @InsertBy, @LastModified);
		SET @JSContentId = SCOPE_IDENTITY()

	INSERT INTO [dbo].[CSSContent] (CSSContent, InsertBy, LastModified)
		VALUES (@CSSContent, @InsertBy, @LastModified);
		SET @CSSContentId = SCOPE_IDENTITY()

	INSERT INTO [dbo].[HTMLContent] (HTMLContent, InsertBy, LastModified)
		VALUES (@HTMLContent, @InsertBy, @LastModified)
		SET @HTMLContentId = SCOPE_IDENTITY()

	INSERT INTO [dbo].[RazerViews] (Location, JSContentId, CSSContentId, HTMLContentId, LastModified)
		VALUES (@Location, @JSContentId, @CSSContentId, @HTMLContentId, @LastModified)

   END
   ELSE
   BEGIN
		UPDATE [dbo].[JSContent] 
		Set JSContent = @JSContent, 
			InsertBy = @InsertBy, 
			LastModified = @LastModified
		where JSContentId = @JSContentId

		UPDATE [dbo].[CSSContent] 
		Set CSSContent = @CSSContent, 
			InsertBy = @InsertBy, 
			LastModified = @LastModified
		where CSSContentId = @CSSContentId

		UPDATE [dbo].[HTMLContent]
		Set HTMLContent = @HTMLContent, 
			InsertBy = @InsertBy, 
			LastModified = @LastModified
		where HTMLContentId = @HTMLContentId
 
		UPDATE [dbo].[RazerViews] 
		set LastModified = @LastModified
	END
GO
PRINT N'Creating [dbo].[GetRazerView]...';


GO
CREATE PROCEDURE [dbo].[GetRazerView]
	@RazerViewId int
AS
	SELECT rv.*, js.JSContent, css.CSSContent, html.HTMLContent from [dbo].[RazerViews] rv
		inner join [dbo].[JSContent] js on js.JSContentId = rv.JSContentId
		inner join [dbo].[CSSContent] css on css.CSSContentId = rv.CSSContentId
		inner join [dbo].[HTMLContent] html on html.HTMLContentId = rv.HTMLContentId
		where RazerViewId = @RazerViewId
	
	UPDATE [dbo].[RazerViews] SET LastRequested = GetUtcDate() WHERE RazerViewId = @RazerViewId;
GO
PRINT N'Creating [dbo].[GetRazerViewByLocation]...';


GO
CREATE PROCEDURE [dbo].[GetRazerViewByLocation]
	@Location nvarchar(max)
AS
	SELECT rv.*, js.JSContent, css.CSSContent, html.HTMLContent from [dbo].[RazerViews] rv
		inner join [dbo].[JSContent] js on js.JSContentId = rv.JSContentId
		inner join [dbo].[CSSContent] css on css.CSSContentId = rv.CSSContentId
		inner join [dbo].[HTMLContent] html on html.HTMLContentId = rv.HTMLContentId
		where Location = @Location

	UPDATE [dbo].[RazerViews] SET LastRequested = GetUtcDate() WHERE Location = @Location;
GO
PRINT N'Creating [dbo].[GetRazerViewLikeLocation]...';


GO
CREATE PROCEDURE [dbo].[GetRazerViewLikeLocation]
	@Location nvarchar(max)
AS
	set @Location = '%' + @Location + '%'
	SELECT rv.*, js.JSContent, css.CSSContent, html.HTMLContent from [dbo].[RazerViews] rv
		inner join [dbo].[JSContent] js on js.JSContentId = rv.JSContentId
		inner join [dbo].[CSSContent] css on css.CSSContentId = rv.CSSContentId
		inner join [dbo].[HTMLContent] html on html.HTMLContentId = rv.HTMLContentId
		where Location like @Location;
	
	UPDATE [dbo].[RazerViews] SET LastRequested = GetUtcDate() WHERE Location like @Location;
GO
PRINT N'Creating [dbo].[UpdateRazerView]...';


GO
CREATE PROCEDURE [dbo].[UpdateRazerView]
	@RazerViewId	int,
	@JSContentId	int,
	@CSSContentId	int,
	@HTMLContentId	int,
	@JSContent		NVARCHAR(MAX),
	@CSSContent		NVARCHAR(MAX),	
	@HTMLContent	NVARCHAR(MAX),
	@Location		NVARCHAR(MAX),
	@InsertBy		NVARCHAR(50),
	@Model			NVARCHAR(100),
	@LastModified	DateTimeOffset

AS

	UPDATE [dbo].[JSContent] 
	Set JSContent = @JSContent, 
		InsertBy = @InsertBy, 
		LastModified = @LastModified
	where JSContentId = @JSContentId

	UPDATE [dbo].[CSSContent] 
	Set CSSContent = @CSSContent, 
		InsertBy = @InsertBy, 
		LastModified = @LastModified
	where CSSContentId = @CSSContentId

	UPDATE [dbo].[HTMLContent]
	Set HTMLContent = @HTMLContent, 
		InsertBy = @InsertBy, 
		LastModified = @LastModified
	where HTMLContentId = @HTMLContentId

	update [dbo].[RazerViews]
	set LastModified	=	GETUTCDATE(),
		Model			=	@Model
	where RazerViewId	=	@RazerViewId;
GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '499c15fd-fadf-4868-8981-c924ea5551f3')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('499c15fd-fadf-4868-8981-c924ea5551f3')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '56f9a1fd-4deb-44f5-a6ec-f15b40fe5f37')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('56f9a1fd-4deb-44f5-a6ec-f15b40fe5f37')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'c5c21826-3f82-439d-aab9-e57a0298031c')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('c5c21826-3f82-439d-aab9-e57a0298031c')

GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
