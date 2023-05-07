--Scaffold-DbContext "Server=localhost\SQLEXPRESS02;Initial Catalog=Trophy;persist security info=True;Integrated Security=SSPI;MultipleActiveResultSets=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir . -Context TrophyDbContext -Force

IF OBJECTPROPERTY(object_id('dbo.[PlayerResult]'), N'IsTable') = 1 DROP TABLE [dbo].[PlayerResult]
GO
IF OBJECTPROPERTY(object_id('dbo.[Player]'), N'IsTable') = 1 DROP TABLE [dbo].[Player]
GO
IF OBJECTPROPERTY(object_id('dbo.[Game]'), N'IsTable') = 1 DROP TABLE [dbo].[Game]
GO

CREATE TABLE [dbo].[Game](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[MatchDate] [smalldatetime] NOT NULL DEFAULT(GETUTCDATE()),
	[Location] [nvarchar](50) NOT NULL,
 CONSTRAINT [Game_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

CREATE TABLE [dbo].[Player](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[Name] [nvarchar](50) NOT NULL
 CONSTRAINT [Player_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

CREATE TABLE [dbo].[PlayerResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL DEFAULT(GETUTCDATE()),
	[InsertByUser] [int] NOT NULL DEFAULT(1),
	[UpdateDate] [datetime2](7) NULL,
	[UpdateByUser] [int] NULL,
	[GameId] [int] NOT NULL,
	[PlayerId] [smallint] NOT NULL,
	[Score] [smallint] NOT NULL,
	[Win] [bit] NOT NULL
 CONSTRAINT [PlayerResult_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ALTER TABLE [dbo].[PlayerResult] WITH CHECK ADD CONSTRAINT [PlayerResult_GameFK] FOREIGN KEY([GameId]) REFERENCES [dbo].[Game] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlayerResult] WITH CHECK ADD CONSTRAINT [PlayerResult_PlayerFK] FOREIGN KEY([PlayerId]) REFERENCES [dbo].[Player] ([Id])
ON DELETE CASCADE
GO