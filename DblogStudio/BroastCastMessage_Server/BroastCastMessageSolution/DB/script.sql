
USE [BroastCastMessageDB]
GO
/****** Object:  Table [dbo].[BCMMessage]    Script Date: 4/1/2015 5:17:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BCMMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](250) NOT NULL,
	[DateCreate] [date] NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_BCMMessage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BCMRegistration]    Script Date: 4/1/2015 5:17:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BCMRegistration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationID] [nvarchar](max) NOT NULL,
	[DateCreate] [date] NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_BCMRegistration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [BroastCastMessageDB] SET  READ_WRITE 
GO
