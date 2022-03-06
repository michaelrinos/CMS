CREATE TABLE [dbo].[RazerViews]
(
	[RazerViewId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Location] NVARCHAR(MAX) NOT NULL, 
    [CSSContentId] INT NOT NULL, 
    [HTMLContentId] INT NOT NULL, 
    [JSContentId] INT NOT NULL, 
    [LastModified] DATETIMEOFFSET NOT NULL

    --CONSTRAINT [FK_Property_To_Table] FOREIGN KEY ([Property]) REFERENCES [dbo].[TableName]([TableProperty])
    --CONSTRAINT [FK_TStorms_To_Ref_TStormTypes] FOREIGN KEY ([TypeId]) REFERENCES [TPR].[Ref_TStormTypes]([TypeId]),
    CONSTRAINT [FK_CSSContentId_To_CSSContent] FOREIGN KEY ([CSSContentId]) REFERENCES [dbo].[CSSContent]([CSSContentId])
    CONSTRAINT [FK_HTMLContentId_To_CSSContent] FOREIGN KEY ([HTMLContentId]) REFERENCES [dbo].[HTMLContent]([HTMLContentId])
    CONSTRAINT [FK_JSContentId_To_CSSContdent] FOREIGN KEY ([JSContentId]) REFERENCES [dbo].[JSContent]([JSContentId]), 
    [LastRequested] DATETIME2 NULL, 
    [Model] NVARCHAR(100) NULL

)
