
/****** Object:  Table [dbo].[tblFromEmail]    Script Date: 14/06/2020 1:58:41 PM ******/
SET ANSI_NULLS ON

CREATE TABLE [dbo].[tblFromEmail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Port] [nvarchar](max) NULL,
	[EnableSsl] [bit] NULL,
	[Host] [nvarchar](max) NULL,
	[DisplayName] [nvarchar](max) NULL,
	[UseDefaultCredentials] [bit] NULL,
 CONSTRAINT [PK_tblFromEmail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[tblFromEmail] ON 

GO
INSERT [dbo].[tblFromEmail] ([ID], [Email], [Password], [Port], [EnableSsl], [Host], [DisplayName], [UseDefaultCredentials]) VALUES (1, N'a4p@alexseeds.com', N'Alex4Pr0gr@mminG', N'26', 0, N'pop.alexseeds.com', N'A4P', 0)
GO
SET IDENTITY_INSERT [dbo].[tblFromEmail] OFF
GO
