Alter Table [tblTransQuotaWithOutContract] 
Drop CONSTRAINT [FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]
----------------------------
GO
Drop table [tblTransQuotaWithOutContract]
-----------------------------
GO

CREATE TABLE [dbo].[tblTransQuotaWithOutContract](
	[TransQuotaWOContractID] [int] NOT NULL identity(1,1),
	[TransID] [int] NOT NULL,
	[QuotaID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_tblTransQuotaWithOutContract] PRIMARY KEY CLUSTERED 
(
	[TransQuotaWOContractID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblTransQuotaWithOutContract]  WITH CHECK ADD  CONSTRAINT [FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract] FOREIGN KEY([QuotaID])
REFERENCES [dbo].[tblDailyQuotaWithOutContract] ([DailyQuotaID])
GO

ALTER TABLE [dbo].[tblTransQuotaWithOutContract] CHECK CONSTRAINT [FK_tblTransQuotaWithOutContract_tblDailyQuotaWithOutContract]
GO


