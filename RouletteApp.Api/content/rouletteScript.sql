USE [master]
GO
/****** Object:  Database [roulette]    Script Date: 26/07/2020 9:54:48 p. m. ******/
CREATE DATABASE [roulette]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'roulette', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\roulette.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'roulette_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\roulette_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [roulette] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [roulette].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [roulette] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [roulette] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [roulette] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [roulette] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [roulette] SET ARITHABORT OFF 
GO
ALTER DATABASE [roulette] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [roulette] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [roulette] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [roulette] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [roulette] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [roulette] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [roulette] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [roulette] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [roulette] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [roulette] SET  DISABLE_BROKER 
GO
ALTER DATABASE [roulette] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [roulette] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [roulette] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [roulette] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [roulette] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [roulette] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [roulette] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [roulette] SET RECOVERY FULL 
GO
ALTER DATABASE [roulette] SET  MULTI_USER 
GO
ALTER DATABASE [roulette] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [roulette] SET DB_CHAINING OFF 
GO
ALTER DATABASE [roulette] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [roulette] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [roulette] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'roulette', N'ON'
GO
ALTER DATABASE [roulette] SET QUERY_STORE = OFF
GO
USE [roulette]
GO
/****** Object:  Table [dbo].[Bet]    Script Date: 26/07/2020 9:54:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bet](
	[Id] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 0) NULL,
	[Color] [nvarchar](50) NULL,
	[Number] [int] NULL,
	[MyBet] [nvarchar](250) NULL,
	[IdRoulette] [int] NULL,
 CONSTRAINT [PK_Bet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consumer]    Script Date: 26/07/2020 9:54:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consumer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Bet] [int] NULL,
	[Money] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Consumer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roulette]    Script Date: 26/07/2020 9:54:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roulette](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaxAmount] [int] NULL,
	[State] [bit] NULL,
 CONSTRAINT [PK_Roulette_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Bet] ([Id], [Amount], [Color], [Number], [MyBet], [IdRoulette]) VALUES (N'4c79f2da-6478-4dfc-9ad6-5c518a439b50', CAST(9000 AS Decimal(18, 0)), N'negro', 31, N'12', 1)
SET IDENTITY_INSERT [dbo].[Consumer] ON 

INSERT [dbo].[Consumer] ([Id], [Name], [Bet], [Money]) VALUES (1, N'Luchi', 0, CAST(10000 AS Decimal(18, 0)))
INSERT [dbo].[Consumer] ([Id], [Name], [Bet], [Money]) VALUES (2, N'Jimmy', 0, CAST(10000 AS Decimal(18, 0)))
INSERT [dbo].[Consumer] ([Id], [Name], [Bet], [Money]) VALUES (3, N'Jair', 0, CAST(10000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Consumer] OFF
SET IDENTITY_INSERT [dbo].[Roulette] ON 

INSERT [dbo].[Roulette] ([Id], [MaxAmount], [State]) VALUES (1, 10000, 0)
INSERT [dbo].[Roulette] ([Id], [MaxAmount], [State]) VALUES (2, 9000, 0)
INSERT [dbo].[Roulette] ([Id], [MaxAmount], [State]) VALUES (3, 11000, 0)
INSERT [dbo].[Roulette] ([Id], [MaxAmount], [State]) VALUES (4, 10000, 0)
SET IDENTITY_INSERT [dbo].[Roulette] OFF
USE [master]
GO
ALTER DATABASE [roulette] SET  READ_WRITE 
GO
