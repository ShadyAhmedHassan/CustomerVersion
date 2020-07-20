USE [SmartScaleDB2020]
GO
/****** Object:  Table [dbo].[tblSlip]    Script Date: 4/23/2020 3:16:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSlip](
	[Slip_ID] [int] IDENTITY(1,1) NOT NULL,
	[Slip_Name] [nvarchar](50) NOT NULL,
	[Slip_MinRange] [float] NOT NULL,
	[Slip_MaxRange] [float] NULL,
	[Slip_Rate] [float] NOT NULL,
	[Slip_IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_tblSlip] PRIMARY KEY CLUSTERED 
(
	[Slip_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[tblSlip] ON 

GO
INSERT [dbo].[tblSlip] ([Slip_ID], [Slip_Name], [Slip_MinRange], [Slip_MaxRange], [Slip_Rate], [Slip_IsDeleted]) VALUES (1, N'‘—ÌÕ…  30', 28, 32, 30, 0)
GO
INSERT [dbo].[tblSlip] ([Slip_ID], [Slip_Name], [Slip_MinRange], [Slip_MaxRange], [Slip_Rate], [Slip_IsDeleted]) VALUES (2, N'‘—ÌÕ… 10', 8, 12, 10, 0)
GO
INSERT [dbo].[tblSlip] ([Slip_ID], [Slip_Name], [Slip_MinRange], [Slip_MaxRange], [Slip_Rate], [Slip_IsDeleted]) VALUES (3, N'‘—ÌÕ… 20', 18, 22, 20, 0)
GO
INSERT [dbo].[tblSlip] ([Slip_ID], [Slip_Name], [Slip_MinRange], [Slip_MaxRange], [Slip_Rate], [Slip_IsDeleted]) VALUES (4, N'»œÊ‰', 1, 1, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[tblSlip] OFF
GO
ALTER TABLE [dbo].[tblSlip] ADD  CONSTRAINT [DF_tblSlip_Slip_IsDeleted]  DEFAULT ((0)) FOR [Slip_IsDeleted]
GO
