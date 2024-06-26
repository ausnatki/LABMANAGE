USE [master]
GO
/****** Object:  Database [LABMANAGE]    Script Date: 2024/6/24 18:21:27 ******/
CREATE DATABASE [LABMANAGE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LABMANAGE', FILENAME = N'D:\电脑软件\SqlServer\MSSQL16.KKKMSSQLSERVER\MSSQL\DATA\LABMANAGE.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LABMANAGE_log', FILENAME = N'D:\电脑软件\SqlServer\MSSQL16.KKKMSSQLSERVER\MSSQL\DATA\LABMANAGE_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LABMANAGE] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LABMANAGE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LABMANAGE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LABMANAGE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LABMANAGE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LABMANAGE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LABMANAGE] SET ARITHABORT OFF 
GO
ALTER DATABASE [LABMANAGE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LABMANAGE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LABMANAGE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LABMANAGE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LABMANAGE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LABMANAGE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LABMANAGE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LABMANAGE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LABMANAGE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LABMANAGE] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LABMANAGE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LABMANAGE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LABMANAGE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LABMANAGE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LABMANAGE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LABMANAGE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LABMANAGE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LABMANAGE] SET RECOVERY FULL 
GO
ALTER DATABASE [LABMANAGE] SET  MULTI_USER 
GO
ALTER DATABASE [LABMANAGE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LABMANAGE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LABMANAGE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LABMANAGE] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LABMANAGE] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LABMANAGE] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'LABMANAGE', N'ON'
GO
ALTER DATABASE [LABMANAGE] SET QUERY_STORE = ON
GO
ALTER DATABASE [LABMANAGE] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LABMANAGE]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Academy]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Academy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_Academy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Floor]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Floor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[IsDel] [bit] NOT NULL,
	[BuildingId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LabAssignments]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LabAssignments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LabID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_LabAssignments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LabEquipmentRepairs]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LabEquipmentRepairs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DLabID] [int] NOT NULL,
	[RepairDate] [datetime2](7) NOT NULL,
	[IssuesFound] [nvarchar](max) NOT NULL,
	[Suggestions] [nvarchar](max) NULL,
	[RepairPersonnelID] [int] NOT NULL,
	[ComletionStatus] [bit] NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_LabEquipmentRepairs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LabInclidentHanding]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LabInclidentHanding](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DLabID] [int] NOT NULL,
	[IncidentDetails] [nvarchar](max) NOT NULL,
	[RepairPersonnelID] [int] NULL,
	[ReportedByID] [int] NOT NULL,
	[InclidentTime] [datetime2](7) NOT NULL,
	[IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_LabInclidentHanding] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Laboratories]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Laboratories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsDel] [bit] NOT NULL,
	[AcademyId] [int] NOT NULL,
	[LabNumber] [nvarchar](max) NOT NULL,
	[FloorId] [int] NOT NULL,
 CONSTRAINT [PK_Laboratories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsDel] [bit] NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Semesters]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Semesters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_Semesters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SingleBuilding]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SingleBuilding](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IsDel] [bit] NOT NULL,
	[Number] [int] NOT NULL,
	[Lng] [nvarchar](max) NULL,
	[Lat] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[LoginName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Sex] [bit] NOT NULL,
	[CID] [int] NOT NULL,
 CONSTRAINT [PK_SysUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DailySafetyCheck]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DailySafetyCheck](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LabID] [int] NOT NULL,
	[SemesterID] [int] NOT NULL,
	[UID] [int] NOT NULL,
	[CheckDate] [datetime2](7) NOT NULL,
	[Cleanliness] [bit] NOT NULL,
	[DoorsAndWindows] [bit] NOT NULL,
	[ElectricalLines] [bit] NOT NULL,
	[FireSafetyEquipmentAvailable] [bit] NOT NULL,
	[FireSafetyEquipmentExpiry] [bit] NOT NULL,
	[InstrumentEquipmentIntact] [bit] NOT NULL,
	[InstrumentEquipmentWorking] [bit] NOT NULL,
	[InstrumentWarningLabelsIntact] [bit] NOT NULL,
	[OtherHazards] [nvarchar](max) NULL,
	[OtherSafetyHazards] [nvarchar](max) NULL,
	[IsDel] [bit] NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_TB_DailySafetyCheck] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 2024/6/24 18:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RID] [int] NOT NULL,
	[UID] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_LabAssignments_LabID]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_LabAssignments_LabID] ON [dbo].[LabAssignments]
(
	[LabID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LabAssignments_UserID]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_LabAssignments_UserID] ON [dbo].[LabAssignments]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LabEquipmentRepairs_DLabID]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_LabEquipmentRepairs_DLabID] ON [dbo].[LabEquipmentRepairs]
(
	[DLabID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LabInclidentHanding_DLabID]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_LabInclidentHanding_DLabID] ON [dbo].[LabInclidentHanding]
(
	[DLabID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Laboratories_AcademyId]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_Laboratories_AcademyId] ON [dbo].[Laboratories]
(
	[AcademyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TB_DailySafetyCheck_LabID]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_TB_DailySafetyCheck_LabID] ON [dbo].[TB_DailySafetyCheck]
(
	[LabID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TB_DailySafetyCheck_SemesterID]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_TB_DailySafetyCheck_SemesterID] ON [dbo].[TB_DailySafetyCheck]
(
	[SemesterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TB_DailySafetyCheck_UID]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_TB_DailySafetyCheck_UID] ON [dbo].[TB_DailySafetyCheck]
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RID]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RID] ON [dbo].[UserRoles]
(
	[RID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_UID]    Script Date: 2024/6/24 18:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_UID] ON [dbo].[UserRoles]
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_DailySafetyCheck] ADD  CONSTRAINT [DF_TB_DailySafetyCheck_State]  DEFAULT ((1)) FOR [State]
GO
ALTER TABLE [dbo].[LabAssignments]  WITH CHECK ADD  CONSTRAINT [FK_LabAssignments_Laboratories_LabID] FOREIGN KEY([LabID])
REFERENCES [dbo].[Laboratories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LabAssignments] CHECK CONSTRAINT [FK_LabAssignments_Laboratories_LabID]
GO
ALTER TABLE [dbo].[LabAssignments]  WITH CHECK ADD  CONSTRAINT [FK_LabAssignments_SysUser_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[SysUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LabAssignments] CHECK CONSTRAINT [FK_LabAssignments_SysUser_UserID]
GO
ALTER TABLE [dbo].[LabEquipmentRepairs]  WITH CHECK ADD  CONSTRAINT [FK_LabEquipmentRepairs_TB_DailySafetyCheck_DLabID] FOREIGN KEY([DLabID])
REFERENCES [dbo].[TB_DailySafetyCheck] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LabEquipmentRepairs] CHECK CONSTRAINT [FK_LabEquipmentRepairs_TB_DailySafetyCheck_DLabID]
GO
ALTER TABLE [dbo].[LabInclidentHanding]  WITH CHECK ADD  CONSTRAINT [FK_LabInclidentHanding_TB_DailySafetyCheck_DLabID] FOREIGN KEY([DLabID])
REFERENCES [dbo].[TB_DailySafetyCheck] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LabInclidentHanding] CHECK CONSTRAINT [FK_LabInclidentHanding_TB_DailySafetyCheck_DLabID]
GO
ALTER TABLE [dbo].[SysUser]  WITH CHECK ADD  CONSTRAINT [FK_SysUser_Academy] FOREIGN KEY([CID])
REFERENCES [dbo].[Academy] ([Id])
GO
ALTER TABLE [dbo].[SysUser] CHECK CONSTRAINT [FK_SysUser_Academy]
GO
ALTER TABLE [dbo].[TB_DailySafetyCheck]  WITH CHECK ADD  CONSTRAINT [FK_TB_DailySafetyCheck_Laboratories_LabID] FOREIGN KEY([LabID])
REFERENCES [dbo].[Laboratories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TB_DailySafetyCheck] CHECK CONSTRAINT [FK_TB_DailySafetyCheck_Laboratories_LabID]
GO
ALTER TABLE [dbo].[TB_DailySafetyCheck]  WITH CHECK ADD  CONSTRAINT [FK_TB_DailySafetyCheck_Semesters_SemesterID] FOREIGN KEY([SemesterID])
REFERENCES [dbo].[Semesters] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TB_DailySafetyCheck] CHECK CONSTRAINT [FK_TB_DailySafetyCheck_Semesters_SemesterID]
GO
ALTER TABLE [dbo].[TB_DailySafetyCheck]  WITH CHECK ADD  CONSTRAINT [FK_TB_DailySafetyCheck_SysUser_UID] FOREIGN KEY([UID])
REFERENCES [dbo].[SysUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TB_DailySafetyCheck] CHECK CONSTRAINT [FK_TB_DailySafetyCheck_SysUser_UID]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RID] FOREIGN KEY([RID])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RID]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_SysUser_UID] FOREIGN KEY([UID])
REFERENCES [dbo].[SysUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_SysUser_UID]
GO
USE [master]
GO
ALTER DATABASE [LABMANAGE] SET  READ_WRITE 
GO
