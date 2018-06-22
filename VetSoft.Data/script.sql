USE [VetSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[LoginByUsernamePassword]    Script Date: 22/6/2018 12:25:23 p. m. ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoginByUsernamePassword]') )   
DROP PROCEDURE [dbo].[LoginByUsernamePassword]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 22/6/2018 12:25:23 p. m. ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Login]') )   
DROP TABLE [dbo].[Login]
GO
USE [master]
GO
/****** Object:  Database [VetSoftDB]    Script Date: 22/6/2018 12:25:23 p. m. ******/
DROP DATABASE [VetSoftDB]
GO
/****** Object:  Database [VetSoftDB]    Script Date: 22/6/2018 12:25:23 p. m. ******/
CREATE DATABASE [VetSoftDB]
Go
USE [VetSoftDB]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 22/6/2018 12:25:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Login] ON 

INSERT [dbo].[Login] ([id], [username], [password]) VALUES (1, N'Admin', N'123456')
SET IDENTITY_INSERT [dbo].[Login] OFF
/****** Object:  StoredProcedure [dbo].[LoginByUsernamePassword]    Script Date: 22/6/2018 12:25:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LoginByUsernamePassword]   
@username varchar(50),   
@password varchar(50)   
AS   
BEGIN   
SELECT id, username, password   
FROM Login   
WHERE username = @username   
AND password = @password   
END   
GO
USE [master]
GO
ALTER DATABASE [VetSoftDB] SET  READ_WRITE 
GO
