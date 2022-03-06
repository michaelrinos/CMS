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
go
