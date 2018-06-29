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
print ('Tabla LOGIN CREADA')
GO

SET IDENTITY_INSERT [dbo].[Login] ON 

INSERT [dbo].[Login] ([id], [username], [password]) VALUES (1, N'Admin', N'123456')
SET IDENTITY_INSERT [dbo].[Login] OFF
print ('Insertado primer Usuario')
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
print ('PROCEDIMIENTO CREADO LOGINBY')
USE [master]
GO
ALTER DATABASE [VetSoftDB] SET  READ_WRITE 
GO
USE [VetSoftDB]
GO
/* DATOS NUEVOS AL DIA 24-06-2018 POR JESUS.*/
/****** Object:  Table [dbo].[Especie]    Script Date: 24/6/2018 19:50:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especie](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Nombre_Esp] [nvarchar](100) NULL,
 CONSTRAINT [PK_Especie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Especie_ESP] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC,
	[Nombre_Esp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
print ('TABLA ESPECIE CREADA')
/****** Object:  Table [dbo].[Raza]    Script Date: 24/6/2018 19:50:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Raza](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[EspecieID] [int] NOT NULL,
 CONSTRAINT [PK_Raza] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Raza]  WITH CHECK ADD  CONSTRAINT [FK_Raza_Especie] FOREIGN KEY([ID])
REFERENCES [dbo].[Especie] ([ID])
GO
ALTER TABLE [dbo].[Raza] CHECK CONSTRAINT [FK_Raza_Especie]
GO

print ('TABLA RAZA CREADA Y SU RELACION CON ESPECIE')

