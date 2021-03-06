USE [MultiSeeds2019]
GO
/****** Object:  ForeignKey [FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]    Script Date: 03/30/2020 14:57:12 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblTransQuotaWithOutContract]'))
ALTER TABLE [dbo].[tblTransQuotaWithOutContract] DROP CONSTRAINT [FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]
GO
/****** Object:  Table [dbo].[tblTransQuotaWithOutContract]    Script Date: 03/30/2020 14:57:12 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblTransQuotaWithOutContract]'))
ALTER TABLE [dbo].[tblTransQuotaWithOutContract] DROP CONSTRAINT [FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblTransQuotaWithOutContract]') AND type in (N'U'))
DROP TABLE [dbo].[tblTransQuotaWithOutContract]
GO
/****** Object:  Table [dbo].[tblDailyQuotaWithOutContract]    Script Date: 03/30/2020 14:57:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDailyQuotaWithOutContract]') AND type in (N'U'))
DROP TABLE [dbo].[tblDailyQuotaWithOutContract]
GO
/****** Object:  Table [dbo].[tblDailyQuotaWOContractGoods]    Script Date: 03/30/2020 14:57:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDailyQuotaWOContractGoods]') AND type in (N'U'))
DROP TABLE [dbo].[tblDailyQuotaWOContractGoods]
GO
/****** Object:  Table [dbo].[tblDailyQuotaWOContractGoods]    Script Date: 03/30/2020 14:57:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDailyQuotaWOContractGoods]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblDailyQuotaWOContractGoods](
	[DailyQuotaGoodID] [int] IDENTITY(1,1) NOT NULL,
	[DailyQuotaID] [int] NOT NULL,
	[GoodID] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblDailyQuotaWithOutContract]    Script Date: 03/30/2020 14:57:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDailyQuotaWithOutContract]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblDailyQuotaWithOutContract](
	[DailyQuotaID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[QuotaDate] [date] NOT NULL,
	[finish] [bit] NOT NULL,
	[Quantity] [decimal](12, 3) NULL,
 CONSTRAINT [PK_tblDailyQuotaWithOutContract] PRIMARY KEY CLUSTERED 
(
	[DailyQuotaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblTransQuotaWithOutContract]    Script Date: 03/30/2020 14:57:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblTransQuotaWithOutContract]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblTransQuotaWithOutContract](
	[TransQuotaWOContractID] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[QuotaID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_tblTransQuotaWithOutContract] PRIMARY KEY CLUSTERED 
(
	[TransQuotaWOContractID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  ForeignKey [FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]    Script Date: 03/30/2020 14:57:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblTransQuotaWithOutContract]'))
ALTER TABLE [dbo].[tblTransQuotaWithOutContract]  WITH CHECK ADD  CONSTRAINT [FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract] FOREIGN KEY([QuotaID])
REFERENCES [dbo].[tblDailyQuotaWithOutContract] ([DailyQuotaID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblTransQuotaWithOutContract]'))
ALTER TABLE [dbo].[tblTransQuotaWithOutContract] CHECK CONSTRAINT [FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]
GO
