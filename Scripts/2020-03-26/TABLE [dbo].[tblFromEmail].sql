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