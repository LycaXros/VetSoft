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
Print('Tabla Login')
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
Print('Usuario Adminstrador Login Insert')
SET IDENTITY_INSERT [dbo].[Login] ON 

INSERT [dbo].[Login] ([id], [username], [password]) VALUES (1, N'Admin', N'TCx52gZ04YU=')
SET IDENTITY_INSERT [dbo].[Login] OFF
/****** Object:  StoredProcedure [dbo].[LoginByUsernamePassword]    Script Date: 22/6/2018 12:25:25 p. m. ******/
Print('Procedimiento de Login')
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LoginByUsernamePassword]   
@username varchar(50),   
@password nvarchar(200)   
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
/* DATOS NUEVOS AL DIA 24-06-2018 POR JESUS.*/
use [VetSoftDB]
GO
/****** Object:  Table [dbo].[Especie]    Script Date: 24/6/2018 19:50:47 ******/
Print('Tabla Especie')
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
/****** Object:  Table [dbo].[Raza]    Script Date: 24/6/2018 19:50:47 ******/
Print('Tabla Raza')
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
Print('Relacion RAZA - Especie')
ALTER TABLE [dbo].[Raza]  WITH CHECK ADD  CONSTRAINT [FK_Raza_Especie] FOREIGN KEY([ID])
REFERENCES [dbo].[Especie] ([ID])
GO
ALTER TABLE [dbo].[Raza] CHECK CONSTRAINT [FK_Raza_Especie]
GO


/* DATOS NUEVOS AL DIA 29-06-2018 POR JESUS.*/
use [VetSoftDB]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 29/6/2018 11:57:58 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paciente](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Color] [nvarchar](100) NOT NULL,
	[Microchip_Licencia] [nvarchar](max) NOT NULL,
	[Genero] [nvarchar](2) NOT NULL,
	[RazaID] [int] NOT NULL,
	[FechaNac] [datetime] NOT NULL,
	[FechaIngreso] [datetime] NOT NULL,
 CONSTRAINT [PK_Paciente] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Propietario]    Script Date: 29/6/2018 11:57:58 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Propietario](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellido] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Telefono] [nvarchar](30) NULL,
 CONSTRAINT [PK_Propietario] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropietarioPaciente]    Script Date: 29/6/2018 11:57:58 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropietarioPaciente](
	[ClienteID] [int] NOT NULL,
	[PacienteID] [int] NOT NULL,
	[Tipo] [int] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_PropietarioPaciente] PRIMARY KEY CLUSTERED 
(
	[ClienteID] ASC,
	[PacienteID] ASC,
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/*INDICES*/
/****** Object:  Index [IX_Especie_ESP]    Script Date: 29/6/2018 11:57:59 a. m. ******/
ALTER TABLE [dbo].[Especie] ADD  CONSTRAINT [IX_Especie_ESP] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC,
	[Nombre_Esp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PacienteGenero]    Script Date: 29/6/2018 11:57:59 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_PacienteGenero] ON [dbo].[Paciente]
(
	[Genero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PacienteNac]    Script Date: 29/6/2018 11:57:59 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_PacienteNac] ON [dbo].[Paciente]
(
	[FechaNac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PacienteNombre]    Script Date: 29/6/2018 11:57:59 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_PacienteNombre] ON [dbo].[Paciente]
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PacienteRaza]    Script Date: 29/6/2018 11:57:59 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_PacienteRaza] ON [dbo].[Paciente]
(
	[RazaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PropietarioNombreApellido]    Script Date: 29/6/2018 11:57:59 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_PropietarioNombreApellido] ON [dbo].[Propietario]
(
	[Nombre] ASC,
	[Apellido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_PropietarioPaciente]    Script Date: 29/6/2018 11:57:59 a. m. ******/

ALTER TABLE [dbo].[Paciente] ADD  CONSTRAINT [DF_Paciente_Genero]  DEFAULT ((0)) FOR [Genero]
GO
ALTER TABLE [dbo].[Paciente]  WITH CHECK ADD  CONSTRAINT [FK_Paciente_Raza] FOREIGN KEY([RazaID])
REFERENCES [dbo].[Raza] ([ID])
GO
ALTER TABLE [dbo].[Paciente] CHECK CONSTRAINT [FK_Paciente_Raza]
GO
ALTER TABLE [dbo].[PropietarioPaciente]  WITH CHECK ADD  CONSTRAINT [FK_PropietarioPaciente_Paciente] FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Paciente] ([ID])
GO
ALTER TABLE [dbo].[PropietarioPaciente] CHECK CONSTRAINT [FK_PropietarioPaciente_Paciente]
GO
ALTER TABLE [dbo].[PropietarioPaciente]  WITH CHECK ADD  CONSTRAINT [FK_PropietarioPaciente_Propietario] FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Propietario] ([ID])
GO
ALTER TABLE [dbo].[PropietarioPaciente] CHECK CONSTRAINT [FK_PropietarioPaciente_Propietario]
GO
ALTER TABLE [dbo].[Raza]  WITH CHECK ADD  CONSTRAINT [FK_Raza_Especie] FOREIGN KEY([EspecieID])
REFERENCES [dbo].[Especie] ([ID])
GO
ALTER TABLE [dbo].[Raza] CHECK CONSTRAINT [FK_Raza_Especie]
GO
ALTER TABLE [dbo].[Paciente]  WITH CHECK ADD  CONSTRAINT [CK_PacienteGenero] CHECK  (([Genero]='F' OR [Genero]='M'))
GO
ALTER TABLE [dbo].[Paciente] CHECK CONSTRAINT [CK_PacienteGenero]
GO
ALTER TABLE [dbo].[PropietarioPaciente]  WITH CHECK ADD  CONSTRAINT [CK_PropietarioPaciente_Tipo] CHECK  (([Tipo]=(3) OR [Tipo]=(2) OR [Tipo]=(1)))
GO
ALTER TABLE [dbo].[PropietarioPaciente] CHECK CONSTRAINT [CK_PropietarioPaciente_Tipo]
GO
/*Descripciones*/
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Color de Pelo o de Piel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Paciente', @level2type=N'COLUMN',@level2name=N'Color'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relacion del Paciente Al Propietario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropietarioPaciente', @level2type=N'CONSTRAINT',@level2name=N'FK_PropietarioPaciente_Paciente'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Relacion del Propietario al Paciente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropietarioPaciente', @level2type=N'CONSTRAINT',@level2name=N'FK_PropietarioPaciente_Propietario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipos de Relacion PacientePropietario:
Propietario Actual -> 1
Propietario Anterior ->2
Propietario Compartido ->3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropietarioPaciente', @level2type=N'CONSTRAINT',@level2name=N'CK_PropietarioPaciente_Tipo'
GO
USE [master]
GO
ALTER DATABASE [VetSoftDB] SET  READ_WRITE 
GO