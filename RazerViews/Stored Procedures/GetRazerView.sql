CREATE PROCEDURE [dbo].[GetRazerView]
	@RazerViewId int
AS
	SELECT rv.*, js.JSContent, css.CSSContent, html.HTMLContent from [dbo].[RazerViews] rv
		inner join [dbo].[JSContent] js on js.JSContentId = rv.JSContentId
		inner join [dbo].[CSSContent] css on css.CSSContentId = rv.CSSContentId
		inner join [dbo].[HTMLContent] html on html.HTMLContentId = rv.HTMLContentId
		where RazerViewId = @RazerViewId
	
	UPDATE [dbo].[RazerViews] SET LastRequested = GetUtcDate() WHERE RazerViewId = @RazerViewId;


