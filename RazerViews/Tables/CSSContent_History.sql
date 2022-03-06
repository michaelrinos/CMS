CREATE TABLE [dbo].[CSSContent_History]
(
    [HistoryId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [HistoryTimestamp] DATETIME NOT NULL,
	[CSSContentId] INT NOT NULL, 
    [CSSContent] NVARCHAR(MAX) NOT NULL, 
    [InsertBy] NVARCHAR(50) NOT NULL, 
    [LastModified] DATETIMEOFFSET NOT NULL
)
