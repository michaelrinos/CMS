CREATE PROCEDURE [dbo].[GetRazerViewCount]
AS
	SELECT count(*) from [dbo].[RazerViews]
