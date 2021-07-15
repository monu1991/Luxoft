USE [Luxoft]
GO

/****** Object:  UserDefinedTableType [dbo].[AssetInput]    Script Date: 7/15/2021 6:10:44 PM ******/
DROP TYPE [dbo].[AssetInput]
GO

/****** Object:  UserDefinedTableType [dbo].[AssetInput]    Script Date: 7/15/2021 6:10:44 PM ******/
CREATE TYPE [dbo].[AssetInput] AS TABLE(
	[AssetId] [int] NOT NULL,
	[IsFixIncome] [bit] NULL DEFAULT ((0)),
	[IsConvertible] [bit] NULL DEFAULT ((0)),
	[IsSwap] [bit] NULL DEFAULT ((0)),
	[IsCash] [bit] NULL DEFAULT ((0)),
	[IsFuture] [bit] NULL DEFAULT ((0)),
	[TimeStamp] [datetime] NOT NULL DEFAULT ((0)),
	PRIMARY KEY CLUSTERED 
(
	[AssetId] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO


