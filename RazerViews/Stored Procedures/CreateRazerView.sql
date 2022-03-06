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
	@Model			NVARCHAR(100),
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
