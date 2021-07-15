USE [Luxoft]
GO

ALTER TABLE [dbo].[Assets] DROP CONSTRAINT [DF_Assets_IsFuture]
GO

ALTER TABLE [dbo].[Assets] DROP CONSTRAINT [DF_Assets_IsCash]
GO

ALTER TABLE [dbo].[Assets] DROP CONSTRAINT [DF_Assets_IsSwap]
GO

ALTER TABLE [dbo].[Assets] DROP CONSTRAINT [DF_Assets_IsConvertible]
GO

ALTER TABLE [dbo].[Assets] DROP CONSTRAINT [DF_Assets_IsFixIncome]
GO

/****** Object:  Table [dbo].[Assets]    Script Date: 7/15/2021 6:12:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Assets]') AND type in (N'U'))
DROP TABLE [dbo].[Assets]
GO

/****** Object:  Table [dbo].[Assets]    Script Date: 7/15/2021 6:12:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Assets](
	[AssetId] [int] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[IsFixIncome] [bit] NOT NULL,
	[IsConvertible] [bit] NOT NULL,
	[IsSwap] [bit] NOT NULL,
	[IsCash] [bit] NOT NULL,
	[IsFuture] [bit] NOT NULL,
 CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
(
	[AssetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Assets] ADD  CONSTRAINT [DF_Assets_IsFixIncome]  DEFAULT ((0)) FOR [IsFixIncome]
GO

ALTER TABLE [dbo].[Assets] ADD  CONSTRAINT [DF_Assets_IsConvertible]  DEFAULT ((0)) FOR [IsConvertible]
GO

ALTER TABLE [dbo].[Assets] ADD  CONSTRAINT [DF_Assets_IsSwap]  DEFAULT ((0)) FOR [IsSwap]
GO

ALTER TABLE [dbo].[Assets] ADD  CONSTRAINT [DF_Assets_IsCash]  DEFAULT ((0)) FOR [IsCash]
GO

ALTER TABLE [dbo].[Assets] ADD  CONSTRAINT [DF_Assets_IsFuture]  DEFAULT ((0)) FOR [IsFuture]
GO


