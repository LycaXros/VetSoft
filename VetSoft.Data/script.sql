USE [VetSoftDB]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropietarioPaciente', @level2type=N'CONSTRAINT',@level2name=N'CK_PropietarioPaciente_Tipo'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropietarioPaciente', @level2type=N'CONSTRAINT',@level2name=N'FK_PropietarioPaciente_Propietario'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PropietarioPaciente', @level2type=N'CONSTRAINT',@level2name=N'FK_PropietarioPaciente_Paciente'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Paciente', @level2type=N'COLUMN',@level2name=N'Color'
GO
/****** Object:  StoredProcedure [dbo].[LoginByUsernamePassword]    Script Date: 7/8/2018 1:57:20 ******/
DROP PROCEDURE [dbo].[LoginByUsernamePassword]
GO
ALTER TABLE [dbo].[PropietarioPaciente] DROP CONSTRAINT [CK_PropietarioPaciente_Tipo]
GO
ALTER TABLE [dbo].[Paciente] DROP CONSTRAINT [CK_PacienteGenero]
GO
ALTER TABLE [dbo].[Raza] DROP CONSTRAINT [FK_Raza_Especie]
GO
ALTER TABLE [dbo].[PropietarioPaciente] DROP CONSTRAINT [FK_PropietarioPaciente_Propietario]
GO
ALTER TABLE [dbo].[PropietarioPaciente] DROP CONSTRAINT [FK_PropietarioPaciente_Paciente]
GO
ALTER TABLE [dbo].[Paciente] DROP CONSTRAINT [FK_Paciente_Raza]
GO
ALTER TABLE [dbo].[Medicamento] DROP CONSTRAINT [FK_Medicamento_Tipo_Med]
GO
ALTER TABLE [dbo].[Medicacion] DROP CONSTRAINT [FK_Medicacion_Medicamento]
GO
ALTER TABLE [dbo].[Medicacion] DROP CONSTRAINT [FK_Medicacion_Chequeo]
GO
ALTER TABLE [dbo].[Cita] DROP CONSTRAINT [FK_Cita_Paciente]
GO
ALTER TABLE [dbo].[Chequeo] DROP CONSTRAINT [FK_Chequeo_Paciente]
GO
ALTER TABLE [dbo].[Paciente] DROP CONSTRAINT [DF_Paciente_Genero]
GO
ALTER TABLE [dbo].[Cita] DROP CONSTRAINT [DF_Cita_VetID]
GO
ALTER TABLE [dbo].[Chequeo] DROP CONSTRAINT [DF_Chequeo_PacienteID]
GO
/****** Object:  Index [IX_Tipo_Med-Nombre]    Script Date: 7/8/2018 1:57:20 ******/
DROP INDEX [IX_Tipo_Med-Nombre] ON [dbo].[Tipo_Med]
GO
/****** Object:  Index [IX_PropietarioNombreApellido]    Script Date: 7/8/2018 1:57:20 ******/
DROP INDEX [IX_PropietarioNombreApellido] ON [dbo].[Propietario]
GO
/****** Object:  Index [IX_Medicamento_TIPO]    Script Date: 7/8/2018 1:57:20 ******/
DROP INDEX [IX_Medicamento_TIPO] ON [dbo].[Medicamento]
GO
/****** Object:  Index [IX_Medicamento]    Script Date: 7/8/2018 1:57:20 ******/
DROP INDEX [IX_Medicamento] ON [dbo].[Medicamento]
GO
/****** Object:  Index [IX_Especie_ESP]    Script Date: 7/8/2018 1:57:20 ******/
ALTER TABLE [dbo].[Especie] DROP CONSTRAINT [IX_Especie_ESP]
GO
/****** Object:  Table [dbo].[Tipo_Med]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Tipo_Med]
GO
/****** Object:  Table [dbo].[Raza]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Raza]
GO
/****** Object:  Table [dbo].[PropietarioPaciente]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[PropietarioPaciente]
GO
/****** Object:  Table [dbo].[Propietario]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Propietario]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Paciente]
GO
/****** Object:  Table [dbo].[Medicamento]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Medicamento]
GO
/****** Object:  Table [dbo].[Medicacion]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Medicacion]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Login]
GO
/****** Object:  Table [dbo].[Especie]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Especie]
GO
/****** Object:  Table [dbo].[Cita]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Cita]
GO
/****** Object:  Table [dbo].[Chequeo]    Script Date: 7/8/2018 1:57:20 ******/
DROP TABLE [dbo].[Chequeo]
GO
USE [master]
GO
/****** Object:  Database [VetSoftDB]    Script Date: 7/8/2018 1:57:20 ******/
DROP DATABASE [VetSoftDB]
GO
/****** Object:  Database [VetSoftDB]    Script Date: 7/8/2018 1:57:20 ******/
CREATE DATABASE [VetSoftDB]
GO
ALTER DATABASE [VetSoftDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VetSoftDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VetSoftDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VetSoftDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VetSoftDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VetSoftDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VetSoftDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [VetSoftDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [VetSoftDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VetSoftDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VetSoftDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VetSoftDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VetSoftDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VetSoftDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VetSoftDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VetSoftDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VetSoftDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VetSoftDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VetSoftDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VetSoftDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VetSoftDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VetSoftDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VetSoftDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VetSoftDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VetSoftDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VetSoftDB] SET  MULTI_USER 
GO
ALTER DATABASE [VetSoftDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VetSoftDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VetSoftDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VetSoftDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VetSoftDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VetSoftDB] SET QUERY_STORE = OFF
GO
USE [VetSoftDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [VetSoftDB]
GO
/****** Object:  Table [dbo].[Chequeo]    Script Date: 7/8/2018 1:57:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chequeo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PacienteID] [int] NOT NULL,
	[VetID] [int] NOT NULL,
	[Prediagnostico] [ntext] NOT NULL,
	[Costo] [float] NOT NULL,
	[Peso] [float] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Observaciones] [ntext] NOT NULL,
 CONSTRAINT [PK_Chequeo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cita]    Script Date: 7/8/2018 1:57:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cita](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PacienteID] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Motivo] [ntext] NOT NULL,
	[VetID] [int] NOT NULL,
 CONSTRAINT [PK_Cita] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Especie]    Script Date: 7/8/2018 1:57:21 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 7/8/2018 1:57:21 ******/
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
/****** Object:  Table [dbo].[Medicacion]    Script Date: 7/8/2018 1:57:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicacion](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [int] NOT NULL,
	[Indicacion] [ntext] NOT NULL,
	[Fecha] [datetime] NULL,
	[ChequeoID] [int] NOT NULL,
 CONSTRAINT [PK_Medicacion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicamento]    Script Date: 7/8/2018 1:57:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicamento](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Descripcion] [ntext] NULL,
	[TipoID] [int] NOT NULL,
 CONSTRAINT [PK_Medicamento] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 7/8/2018 1:57:21 ******/
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
	[Estado] [nvarchar](25) NULL,
 CONSTRAINT [PK_Paciente] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Propietario]    Script Date: 7/8/2018 1:57:21 ******/
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
	[Telefono_2] [nvarchar](30) NULL,
	[Direccion] [ntext] NULL,
 CONSTRAINT [PK_Propietario] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropietarioPaciente]    Script Date: 7/8/2018 1:57:21 ******/
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
/****** Object:  Table [dbo].[Raza]    Script Date: 7/8/2018 1:57:21 ******/
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
/****** Object:  Table [dbo].[Tipo_Med]    Script Date: 7/8/2018 1:57:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Med](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Tipo_Med] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Especie] ON 
GO
INSERT [dbo].[Especie] ([ID], [Nombre], [Nombre_Esp]) VALUES (2, N'Mantis', N'Religiosa')
GO
INSERT [dbo].[Especie] ([ID], [Nombre], [Nombre_Esp]) VALUES (1, N'Perro', NULL)
GO
INSERT [dbo].[Especie] ([ID], [Nombre], [Nombre_Esp]) VALUES (10, N'prueba', NULL)
GO
SET IDENTITY_INSERT [dbo].[Especie] OFF
GO
SET IDENTITY_INSERT [dbo].[Login] ON 
GO
INSERT [dbo].[Login] ([id], [username], [password]) VALUES (1, N'Admin', N'TCx52gZ04YU=')
GO
SET IDENTITY_INSERT [dbo].[Login] OFF
GO
SET IDENTITY_INSERT [dbo].[Medicamento] ON 
GO
INSERT [dbo].[Medicamento] ([ID], [Nombre], [Descripcion], [TipoID]) VALUES (1, N'DHCP', N'Vacuna ', 1)
GO
INSERT [dbo].[Medicamento] ([ID], [Nombre], [Descripcion], [TipoID]) VALUES (2, N'Antitetanos', NULL, 1)
GO
INSERT [dbo].[Medicamento] ([ID], [Nombre], [Descripcion], [TipoID]) VALUES (3, N'Abezonal', NULL, 2)
GO
SET IDENTITY_INSERT [dbo].[Medicamento] OFF
GO
SET IDENTITY_INSERT [dbo].[Raza] ON 
GO
INSERT [dbo].[Raza] ([ID], [Nombre], [EspecieID]) VALUES (1, N'Perro Salchicha', 1)
GO
INSERT [dbo].[Raza] ([ID], [Nombre], [EspecieID]) VALUES (2, N'Pastor Aleman', 1)
GO
INSERT [dbo].[Raza] ([ID], [Nombre], [EspecieID]) VALUES (3, N'Mantis Comun', 2)
GO
SET IDENTITY_INSERT [dbo].[Raza] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipo_Med] ON 
GO
INSERT [dbo].[Tipo_Med] ([ID], [Nombre]) VALUES (2, N'Desparasitante')
GO
INSERT [dbo].[Tipo_Med] ([ID], [Nombre]) VALUES (1, N'Vacuna')
GO
SET IDENTITY_INSERT [dbo].[Tipo_Med] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Especie_ESP]    Script Date: 7/8/2018 1:57:21 ******/
ALTER TABLE [dbo].[Especie] ADD  CONSTRAINT [IX_Especie_ESP] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC,
	[Nombre_Esp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Medicamento]    Script Date: 7/8/2018 1:57:21 ******/
CREATE NONCLUSTERED INDEX [IX_Medicamento] ON [dbo].[Medicamento]
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Medicamento_TIPO]    Script Date: 7/8/2018 1:57:21 ******/
CREATE NONCLUSTERED INDEX [IX_Medicamento_TIPO] ON [dbo].[Medicamento]
(
	[TipoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PropietarioNombreApellido]    Script Date: 7/8/2018 1:57:21 ******/
CREATE NONCLUSTERED INDEX [IX_PropietarioNombreApellido] ON [dbo].[Propietario]
(
	[Nombre] ASC,
	[Apellido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tipo_Med-Nombre]    Script Date: 7/8/2018 1:57:21 ******/
CREATE NONCLUSTERED INDEX [IX_Tipo_Med-Nombre] ON [dbo].[Tipo_Med]
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chequeo] ADD  CONSTRAINT [DF_Chequeo_PacienteID]  DEFAULT ((0)) FOR [PacienteID]
GO
ALTER TABLE [dbo].[Cita] ADD  CONSTRAINT [DF_Cita_VetID]  DEFAULT ((0)) FOR [VetID]
GO
ALTER TABLE [dbo].[Paciente] ADD  CONSTRAINT [DF_Paciente_Genero]  DEFAULT ((0)) FOR [Genero]
GO
ALTER TABLE [dbo].[Chequeo]  WITH CHECK ADD  CONSTRAINT [FK_Chequeo_Paciente] FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Paciente] ([ID])
GO
ALTER TABLE [dbo].[Chequeo] CHECK CONSTRAINT [FK_Chequeo_Paciente]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Paciente] FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Paciente] ([ID])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [FK_Cita_Paciente]
GO
ALTER TABLE [dbo].[Medicacion]  WITH CHECK ADD  CONSTRAINT [FK_Medicacion_Chequeo] FOREIGN KEY([ChequeoID])
REFERENCES [dbo].[Chequeo] ([ID])
GO
ALTER TABLE [dbo].[Medicacion] CHECK CONSTRAINT [FK_Medicacion_Chequeo]
GO
ALTER TABLE [dbo].[Medicacion]  WITH CHECK ADD  CONSTRAINT [FK_Medicacion_Medicamento] FOREIGN KEY([Tipo])
REFERENCES [dbo].[Medicamento] ([ID])
GO
ALTER TABLE [dbo].[Medicacion] CHECK CONSTRAINT [FK_Medicacion_Medicamento]
GO
ALTER TABLE [dbo].[Medicamento]  WITH CHECK ADD  CONSTRAINT [FK_Medicamento_Tipo_Med] FOREIGN KEY([TipoID])
REFERENCES [dbo].[Tipo_Med] ([ID])
GO
ALTER TABLE [dbo].[Medicamento] CHECK CONSTRAINT [FK_Medicamento_Tipo_Med]
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
ALTER TABLE [dbo].[Paciente]  WITH CHECK ADD  CONSTRAINT [CK_PacienteGenero] CHECK  (([Genero]='F' OR [Genero]='M' OR [Genero]='H'))
GO
ALTER TABLE [dbo].[Paciente] CHECK CONSTRAINT [CK_PacienteGenero]
GO
ALTER TABLE [dbo].[PropietarioPaciente]  WITH CHECK ADD  CONSTRAINT [CK_PropietarioPaciente_Tipo] CHECK  (([Tipo]=(3) OR [Tipo]=(2) OR [Tipo]=(1)))
GO
ALTER TABLE [dbo].[PropietarioPaciente] CHECK CONSTRAINT [CK_PropietarioPaciente_Tipo]
GO
/****** Object:  StoredProcedure [dbo].[LoginByUsernamePassword]    Script Date: 7/8/2018 1:57:21 ******/
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