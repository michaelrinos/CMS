CREATE TABLE [dbo].[JSContent_History]
(
    [HistoryId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [HistoryTimestamp] DATETIME NOT NULL,
	[JSContentId] INT NOT NULL, 
    [JSContent] NVARCHAR(MAX) NOT NULL, 
    [InsertBy] NVARCHAR(50) NOT NULL, 
    [LastModified] DATETIMEOFFSET NOT NULL
)
