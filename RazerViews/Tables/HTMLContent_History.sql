CREATE TABLE [dbo].[HTMLContent_History]
(
    [HistoryId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [HistoryTimestamp] DATETIME NOT NULL,
	[HTMLContentId] INT NOT NULL, 
    [HTMLContent] NVARCHAR(MAX) NOT NULL, 
    [InsertBy] NVARCHAR(50) NOT NULL, 
    [LastModified] DATETIMEOFFSET NOT NULL
)
