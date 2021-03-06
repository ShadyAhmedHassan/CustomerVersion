USE [MultiSeeds2019]
GO
/****** Object:  Table [dbo].[tblPermission]    Script Date: 03/30/2020 21:00:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPermission]') AND type in (N'U'))
DROP TABLE [dbo].[tblPermission]
GO
/****** Object:  Table [dbo].[tblPermission]    Script Date: 03/30/2020 21:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPermission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblPermission](
	[Permission_ID] [int] NOT NULL,
	[Permission_Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_tblPermission] PRIMARY KEY CLUSTERED 
(
	[Permission_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (1, N'بيـانـات السيـارات')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (2, N'بيـانـات المقطـورات')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (3, N'المـدن')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (4, N'بيـانـات السـائـقين')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (5, N'بيـانـات العمـلاء')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (6, N'أنواع الحمـولــه')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (7, N'بيـانـات الشـركـه')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (8, N'المرسـل إليــه')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (9, N'بيـانـات الموازين')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (10, N'بيـانـات المستخـدمـين')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (11, N'صلاحيــات المستخـدمـين')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (12, N'الورادى')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (13, N'الشـرائح')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (14, N'العقود')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (15, N'المطالبات')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (16, N'الكوته اليوميه')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (17, N'نوع البروتين')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (18, N'نوع الصنف')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (19, N'كوته يوميه بدون عقد')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (101, N'التقارير')
INSERT [dbo].[tblPermission] ([Permission_ID], [Permission_Name]) VALUES (102, N'العمليات')
