CREATE DATABASE [Courses_Platform]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Courses_Platform', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL12.LENOVOSQLSERVER\MSSQL\DATA\Courses_Platform.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Courses_Platform_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL12.LENOVOSQLSERVER\MSSQL\DATA\Courses_Platform_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
USE [Courses_Platform]
CREATE TABLE [Branch](
	[id] [bigint] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[center_id] [bigint] NOT NULL,
	[lat] [float] NULL,
	[lng] [float] NULL,
	[address] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
	[edit_date] [datetime] NOT NULL,
	[is_blocked] [bit] NOT NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Center](
	[id] [bigint] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[about] [nvarchar](max) NULL,
	[email] [nvarchar](100) NOT NULL,
	[website] [nvarchar](150) NULL,
	[fb_page] [nvarchar](200) NULL,
	[instagram_page] [nvarchar](200) NULL,
	[twitter_page] [nvarchar](200) NULL,
	[date] [datetime] NOT NULL,
	[edit_date] [datetime] NOT NULL,
	[is_blocked] [bit] NOT NULL,
 CONSTRAINT [PK_Center] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CenterTag](
	[id] [bigint] NOT NULL,
	[center_id] [bigint] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CenterTag] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneNumber](
	[id] [bigint] NOT NULL,
	[fk_id] [bigint] NOT NULL,
	[table_type] [int] NOT NULL,
	[phone_number] [nvarchar](25) NOT NULL,
	[phone_type] [int] NOT NULL,
 CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Specialization](
	[id] [bigint] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Specialization] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SpecializationCenter](
	[id] [bigint] NOT NULL,
	[center_id] [bigint] NOT NULL,
	[specialization_id] [bigint] NOT NULL,
 CONSTRAINT [PK_SpecializationCenter] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SpecializationTag](
	[id] [bigint] NOT NULL,
	[specialization_id] [bigint] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SpecializationTag] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User](
	[id] [bigint] NOT NULL,
	[username] [nvarchar](100) NOT NULL,
	[password] [nvarchar](150) NOT NULL,
	[type] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Branch] ADD  CONSTRAINT [DF_Branch_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [Branch] ADD  CONSTRAINT [DF_Branch_edit_date]  DEFAULT (getdate()) FOR [edit_date]
GO
ALTER TABLE [Branch] ADD  CONSTRAINT [DF_Branch_is_blocked]  DEFAULT ((1)) FOR [is_blocked]
GO
ALTER TABLE [Center] ADD  CONSTRAINT [DF_Center_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [Center] ADD  CONSTRAINT [DF_Center_edit_date]  DEFAULT (getdate()) FOR [edit_date]
GO
ALTER TABLE [Center] ADD  CONSTRAINT [DF_Center_is_blocked]  DEFAULT ((1)) FOR [is_blocked]
GO
ALTER TABLE [Branch]  WITH CHECK ADD  CONSTRAINT [FK_Branch_Center] FOREIGN KEY([center_id])
REFERENCES [Center] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [Branch] CHECK CONSTRAINT [FK_Branch_Center]
GO
ALTER TABLE [CenterTag]  WITH CHECK ADD  CONSTRAINT [FK_CenterTag_Center] FOREIGN KEY([center_id])
REFERENCES [Center] ([id])
GO
ALTER TABLE [CenterTag] CHECK CONSTRAINT [FK_CenterTag_Center]
GO
ALTER TABLE [SpecializationCenter]  WITH CHECK ADD  CONSTRAINT [FK_SpecializationCenter_Center] FOREIGN KEY([center_id])
REFERENCES [Center] ([id])
GO
ALTER TABLE [SpecializationCenter] CHECK CONSTRAINT [FK_SpecializationCenter_Center]
GO
ALTER TABLE [SpecializationCenter]  WITH CHECK ADD  CONSTRAINT [FK_SpecializationCenter_Specialization] FOREIGN KEY([specialization_id])
REFERENCES [Specialization] ([id])
GO
ALTER TABLE [SpecializationCenter] CHECK CONSTRAINT [FK_SpecializationCenter_Specialization]
GO
ALTER TABLE [SpecializationTag]  WITH CHECK ADD  CONSTRAINT [FK_SpecializationTag_Specialization] FOREIGN KEY([specialization_id])
REFERENCES [Specialization] ([id])
GO
ALTER TABLE [SpecializationTag] CHECK CONSTRAINT [FK_SpecializationTag_Specialization]
GO
USE [master]
GO
ALTER DATABASE [Courses_Platform] SET  READ_WRITE 
GO
