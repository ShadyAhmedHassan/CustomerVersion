USE [MultiSeeds2019]
GO
/****** Object:  Table [dbo].[tblDailyQuotaWithOutContract]    Script Date: 30/03/2020 08:57:33 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDailyQuotaWithOutContract](
	[DailyQuotaID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[QuotaDate] [date] NOT NULL,
	[finish] [bit] NOT NULL,
 CONSTRAINT [PK_tblDailyQuotaWithOutContract] PRIMARY KEY CLUSTERED 
(
	[DailyQuotaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDailyQuotaWOContractGoods]    Script Date: 30/03/2020 08:57:33 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDailyQuotaWOContractGoods](
	[DailyQuotaGoodID] [int] IDENTITY(1,1) NOT NULL,
	[DailyQuotaID] [int] NOT NULL,
	[GoodID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblDailyQuotaWOContractGoods] PRIMARY KEY CLUSTERED 
(
	[DailyQuotaID] ASC,
	[GoodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[tblDailyQuotaWOContractGoods]  WITH CHECK ADD  CONSTRAINT [FK_tblDailyQuotaWOContractGoods_tblDailyQuotaWithOutContract] FOREIGN KEY([DailyQuotaGoodID])
REFERENCES [dbo].[tblDailyQuotaWithOutContract] ([DailyQuotaID])
GO
ALTER TABLE [dbo].[tblDailyQuotaWOContractGoods] CHECK CONSTRAINT [FK_tblDailyQuotaWOContractGoods_tblDailyQuotaWithOutContract]
GO
ALTER TABLE [dbo].[tblDailyQuotaWOContractGoods]  WITH CHECK ADD  CONSTRAINT [FK_tblDailyQuotaWOContractGoods_tblGood] FOREIGN KEY([GoodID])
REFERENCES [dbo].[tblGood] ([Good_ID])
GO
ALTER TABLE [dbo].[tblDailyQuotaWOContractGoods] CHECK CONSTRAINT [FK_tblDailyQuotaWOContractGoods_tblGood]
GO
Alter Table [tblDailyQuotaWithOutContract]
ADD Quantity decimal(12,3)
GO
