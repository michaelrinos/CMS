CREATE TABLE [dbo].[Views](
	[Location] [nvarchar](150) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[LastModified] [datetime] NOT NULL,
    [LastRequested] [datetime]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

insert into [SportsStore].[dbo].[Views] (Location, LastModified, Content)
  Values ( N'/NotSpecified.cshtml', GETDATE(), N'<h1>
Oops.
</h1>
<p>
Looks like you forgot to specify which view we should show you.
</p>')

insert into [SportsStore].[dbo].[Views] (location, LastModified, Content)
	Values( N'/Editor.cshtml', GETDATE(),  N'<style>
    body {
        text-align: center;
    }

    textarea {
        width: 32%;
        float: top;
        min-height: 250px;
        overflow: scroll;
        margin: auto;
        display: inline-block;
        background: #f4f4f9;
        outline: none;
        font-family: Courier, sans-serif;
        font-size: 14px;
    }

    iframe {
        bottom: 0;
        position: relative;
        width: 100%;
        height: 35em;
    }
</style>
<html>
<head>
    <title>Code Editor</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <textarea id="html" placeholder="HTML"></textarea>      <textarea id="css" placeholder="CSS"></textarea>      <textarea id="js" placeholder="JavaScript"></textarea>      <iframe id="code"></iframe>
    <script type="text/javascript" src="app.js"></script>
</body>
</html>
<script>
    function compile() {
        var html = document.getElementById("html");
        var css = document.getElementById("css");
        var js = document.getElementById("js");
        var code = document.getElementById("code").contentWindow.document;
        document.body.onkeyup = function () {
            code.open();
            code.writeln(html.value + "<style>" + css.value + "</style>" + "<script>" + js.value + "<//script>");
            code.close();
        };
    }
    compile();</script>' where Location = N'/Editor.cshtml'
