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
USE [$(DatabaseName)];


GO
PRINT N'Altering [dbo].[CreateRazerView]...';


GO
ALTER PROCEDURE [dbo].[CreateRazerView]
	@RazerViewId	int,
	@JSContentId	int,
	@CSSContentId	int,
	@HTMLContentId	int,
	@JSContent		NVARCHAR(MAX),
	@CSSContent		NVARCHAR(MAX),	
	@HTMLContent	NVARCHAR(MAX),
	@Location		NVARCHAR(100),
	@InsertBy		NVARCHAR(50)

AS

	DECLARE @LastModified	DATETIME = GETUTCDATE()


	INSERT INTO [dbo].[JSContent] (JSContent, InsertBy, LastModified)
		VALUES (@JSContent, @InsertBy, @LastModified);

	INSERT INTO [dbo].[CSSContent] (CSSContent, InsertBy, LastModified)
		VALUES (@CSSContent, @InsertBy, @LastModified);

	INSERT INTO [dbo].[HTMLContent] (HTMLContent, InsertBy, LastModified)
		VALUES (@HTMLContent, @InsertBy, @LastModified)

	INSERT INTO [dbo].[RazerViews] (JSContentId, CSSContentId, HTMLContentId, LastModified)
		VALUES (@JSContentId, @CSSContentId, @HTMLContentId, @LastModified)
GO
PRINT N'Update complete.';


GO
