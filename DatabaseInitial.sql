----CREATE DATABASE [University_CRUD]
----go
--USE [University_CRUD]
--GO
--/****** Object:  Table [dbo].[Courses]    Script Date: 6/27/2018 9:40:04 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Courses](
--	[Id] [bigint] IDENTITY(1,1) NOT NULL,
--	[Name] [nvarchar](50) NOT NULL,
--	[Credits] [int] NOT NULL,
-- CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
--(
--	[Id] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY]

--GO
--/****** Object:  Table [dbo].[Disenrollments]    Script Date: 6/27/2018 9:40:04 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Disenrollments](
--	[DisenrollmentID] [bigint] IDENTITY(1,1) NOT NULL,
--	[Id] [bigint] NOT NULL,
--	[StudentID] [bigint] NOT NULL,
--	[DateTime] [datetime] NOT NULL,
--	[Comment] [nvarchar](max) NOT NULL,
-- CONSTRAINT [PK_Disenrollment] PRIMARY KEY CLUSTERED 
--(
--	[DisenrollmentID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

--GO
--/****** Object:  Table [dbo].[Enrollments]    Script Date: 6/27/2018 9:40:04 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Enrollments](
--	[EnrollmentID] [bigint] IDENTITY(1,1) NOT NULL,
--	[StudentID] [bigint] NOT NULL,
--	[Id] [bigint] NOT NULL,
--	[Grade] [int] NOT NULL,
-- CONSTRAINT [PK_Enrollment] PRIMARY KEY CLUSTERED 
--(
--	[EnrollmentID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY]

--GO
--/****** Object:  Table [dbo].[Students]    Script Date: 6/27/2018 9:40:04 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[Students](
--	[StudentID] [bigint] IDENTITY(1,1) NOT NULL,
--	[Name] [nvarchar](50) NOT NULL,
--	[Email] [nvarchar](50) NOT NULL,
-- CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
--(
--	[StudentID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY]

--GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

GO
INSERT [dbo].[Courses] ([Id], [Name], [Credits]) VALUES (1, N'Calculus', 3)
GO
INSERT [dbo].[Courses] ([Id], [Name], [Credits]) VALUES (2, N'Chemistry', 3)
GO
INSERT [dbo].[Courses] ([Id], [Name], [Credits]) VALUES (3, N'Composition', 3)
GO
INSERT [dbo].[Courses] ([Id], [Name], [Credits]) VALUES (4, N'Literature', 4)
GO
INSERT [dbo].[Courses] ([Id], [Name], [Credits]) VALUES (5, N'Trigonometry', 4)
GO
INSERT [dbo].[Courses] ([Id], [Name], [Credits]) VALUES (6, N'Microeconomics', 3)
GO
INSERT [dbo].[Courses] ([Id], [Name], [Credits]) VALUES (7, N'Macroeconomics', 3)
GO
SET IDENTITY_INSERT [dbo].[Courses] OFF
--GO
--SET IDENTITY_INSERT [dbo].[Enrollments] ON 

--GO
--INSERT [dbo].[Enrollments] ([EnrollmentID], [StudentID], [Id], [Grade]) VALUES (5, 2, 2, 1)
--GO
--INSERT [dbo].[Enrollments] ([EnrollmentID], [StudentID], [Id], [Grade]) VALUES (13, 2, 3, 3)
--GO
--INSERT [dbo].[Enrollments] ([EnrollmentID], [StudentID], [Id], [Grade]) VALUES (20, 1, 1, 1)
--GO
--INSERT [dbo].[Enrollments] ([EnrollmentID], [StudentID], [Id], [Grade]) VALUES (38, 1, 2, 3)
--GO
--SET IDENTITY_INSERT [dbo].[Enrollments] OFF
--GO
--SET IDENTITY_INSERT [dbo].[Students] ON 

--GO
--INSERT [dbo].[Students] ([StudentID], [Name], [Email]) VALUES (1, N'Alice', N'alice@gmail.com')
--GO
--INSERT [dbo].[Students] ([StudentID], [Name], [Email]) VALUES (2, N'Bob', N'bob@outlook.com')
--GO
--SET IDENTITY_INSERT [dbo].[Students] OFF
--GO
--ALTER TABLE [dbo].[Disenrollments]  WITH CHECK ADD  CONSTRAINT [FK_Disenrollment_Course] FOREIGN KEY([Id])
--REFERENCES [dbo].[Courses] ([Id])
--GO
--ALTER TABLE [dbo].[Disenrollments] CHECK CONSTRAINT [FK_Disenrollment_Course]
--GO
--ALTER TABLE [dbo].[Disenrollments]  WITH CHECK ADD  CONSTRAINT [FK_Disenrollment_Student] FOREIGN KEY([StudentID])
--REFERENCES [dbo].[Students] ([StudentID])
--GO
--ALTER TABLE [dbo].[Disenrollments] CHECK CONSTRAINT [FK_Disenrollment_Student]
--GO
--ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Course] FOREIGN KEY([Id])
--REFERENCES [dbo].[Courses] ([Id])
--GO
--ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK_Enrollment_Course]
--GO
--ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Student] FOREIGN KEY([StudentID])
--REFERENCES [dbo].[Students] ([StudentID])
--GO
--ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK_Enrollment_Student]
--GO
