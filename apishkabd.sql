USE [WebLibrary]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 08.10.2024 11:52:01 ******/
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
/****** Object:  Table [dbo].[BookExemplar]    Script Date: 08.10.2024 11:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookExemplar](
	[Exemplar_Id] [int] IDENTITY(1,1) NOT NULL,
	[Books_Count] [int] NOT NULL,
	[Book_Id] [int] NOT NULL,
	[Exemplar_Price] [int] NOT NULL,
 CONSTRAINT [PK_BookExemplar] PRIMARY KEY CLUSTERED 
(
	[Exemplar_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 08.10.2024 11:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id_Book] [int] IDENTITY(1,1) NOT NULL,
	[Author] [nvarchar](max) NOT NULL,
	[Id_Genre] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Year] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id_Book] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 08.10.2024 11:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[Id_Genre] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[Id_Genre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Readers]    Script Date: 08.10.2024 11:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Readers](
	[Id_User] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Date_Birth] [datetime2](7) NOT NULL,
	[Login] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Id_Role] [int] NULL,
 CONSTRAINT [PK_Readers] PRIMARY KEY CLUSTERED 
(
	[Id_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentHistory]    Script Date: 08.10.2024 11:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentHistory](
	[id_Rent] [int] IDENTITY(1,1) NOT NULL,
	[Rental_Start] [datetime2](7) NOT NULL,
	[Rental_Time] [int] NOT NULL,
	[Id_Reader] [int] NOT NULL,
	[Id_Book] [int] NOT NULL,
	[Rental_End] [datetime2](7) NOT NULL,
	[Rental_Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RentHistory] PRIMARY KEY CLUSTERED 
(
	[id_Rent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 08.10.2024 11:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id_Role] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id_Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240929143424_aaaa1', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240929183526_zaebala', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[BookExemplar] ON 

INSERT [dbo].[BookExemplar] ([Exemplar_Id], [Books_Count], [Book_Id], [Exemplar_Price]) VALUES (4, 2, 5, 1200)
INSERT [dbo].[BookExemplar] ([Exemplar_Id], [Books_Count], [Book_Id], [Exemplar_Price]) VALUES (5, 3, 7, 2100)
INSERT [dbo].[BookExemplar] ([Exemplar_Id], [Books_Count], [Book_Id], [Exemplar_Price]) VALUES (6, 1, 8, 1000)
INSERT [dbo].[BookExemplar] ([Exemplar_Id], [Books_Count], [Book_Id], [Exemplar_Price]) VALUES (1002, 5, 1003, 131313)
INSERT [dbo].[BookExemplar] ([Exemplar_Id], [Books_Count], [Book_Id], [Exemplar_Price]) VALUES (1003, 12, 6, 3100)
SET IDENTITY_INSERT [dbo].[BookExemplar] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id_Book], [Author], [Id_Genre], [Title], [Description], [Year]) VALUES (5, N'Говоров', 3, N'Горе не в уме', N'sвввывывы', CAST(N'2024-09-27T15:21:00.1920000' AS DateTime2))
INSERT [dbo].[Books] ([Id_Book], [Author], [Id_Genre], [Title], [Description], [Year]) VALUES (6, N'Кронштейн', 2, N'дракончики', N'алавтал', CAST(N'2021-04-04T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Books] ([Id_Book], [Author], [Id_Genre], [Title], [Description], [Year]) VALUES (7, N'Кулибякин', 3, N'пропавший свидетель', N'ущаылв', CAST(N'2023-05-09T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Books] ([Id_Book], [Author], [Id_Genre], [Title], [Description], [Year]) VALUES (8, N'Толстой', 3, N'Война и мир', N'ваава', CAST(N'2024-09-27T14:26:40.4090000' AS DateTime2))
INSERT [dbo].[Books] ([Id_Book], [Author], [Id_Genre], [Title], [Description], [Year]) VALUES (1002, N'Бродский', 5, N'Хроники нарнии', N'льауаь', CAST(N'2024-05-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Books] ([Id_Book], [Author], [Id_Genre], [Title], [Description], [Year]) VALUES (1003, N'Кинг', 4, N'зеленый слоник', N'альскш', CAST(N'2020-02-10T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Genre] ON 

INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (1, N'Фантастика')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (2, N'Дэтектив')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (3, N'Романтика')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (4, N'Научпоп')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (5, N'Ужасы')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (6, N'Приключения')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (7, N'Биография')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (8, N'История')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (9, N'Поэзия')
INSERT [dbo].[Genre] ([Id_Genre], [Name]) VALUES (1002, N'manhwa')
SET IDENTITY_INSERT [dbo].[Genre] OFF
GO
SET IDENTITY_INSERT [dbo].[Readers] ON 

INSERT [dbo].[Readers] ([Id_User], [Name], [Date_Birth], [Login], [Password], [Id_Role]) VALUES (3, N'fara', CAST(N'2006-12-11T00:00:00.0000000' AS DateTime2), N'fara', N'123', 1)
INSERT [dbo].[Readers] ([Id_User], [Name], [Date_Birth], [Login], [Password], [Id_Role]) VALUES (4, N'user', CAST(N'2010-01-01T00:00:00.0000000' AS DateTime2), N'user', N'123', 2)
INSERT [dbo].[Readers] ([Id_User], [Name], [Date_Birth], [Login], [Password], [Id_Role]) VALUES (5, N'test', CAST(N'2024-09-29T17:24:08.3420000' AS DateTime2), N'test', N'test', 1)
INSERT [dbo].[Readers] ([Id_User], [Name], [Date_Birth], [Login], [Password], [Id_Role]) VALUES (7, N'misha', CAST(N'2024-09-29T17:30:03.7810000' AS DateTime2), N'misha', N'misha', 2)
INSERT [dbo].[Readers] ([Id_User], [Name], [Date_Birth], [Login], [Password], [Id_Role]) VALUES (8, N'ivan', CAST(N'2010-10-01T00:00:00.0000000' AS DateTime2), N'ivan', N'ivan', 2)
INSERT [dbo].[Readers] ([Id_User], [Name], [Date_Birth], [Login], [Password], [Id_Role]) VALUES (1008, N'name', CAST(N'2006-12-11T00:00:00.0000000' AS DateTime2), N'name', N'name', 2)
SET IDENTITY_INSERT [dbo].[Readers] OFF
GO
SET IDENTITY_INSERT [dbo].[RentHistory] ON 

INSERT [dbo].[RentHistory] ([id_Rent], [Rental_Start], [Rental_Time], [Id_Reader], [Id_Book], [Rental_End], [Rental_Status]) VALUES (1, CAST(N'2024-09-29T22:01:46.6330313' AS DateTime2), 14, 3, 5, CAST(N'2024-10-13T22:01:46.6330925' AS DateTime2), N'нет')
INSERT [dbo].[RentHistory] ([id_Rent], [Rental_Start], [Rental_Time], [Id_Reader], [Id_Book], [Rental_End], [Rental_Status]) VALUES (2, CAST(N'2024-09-29T22:02:58.5475305' AS DateTime2), 33, 3, 7, CAST(N'2024-11-01T22:02:58.5475326' AS DateTime2), N'нет')
INSERT [dbo].[RentHistory] ([id_Rent], [Rental_Start], [Rental_Time], [Id_Reader], [Id_Book], [Rental_End], [Rental_Status]) VALUES (1002, CAST(N'2024-10-01T08:44:22.9637793' AS DateTime2), 14, 8, 1003, CAST(N'2024-10-15T08:44:22.9638691' AS DateTime2), N'да')
SET IDENTITY_INSERT [dbo].[RentHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id_Role], [Name]) VALUES (1, N'admin')
INSERT [dbo].[Roles] ([Id_Role], [Name]) VALUES (2, N'reader')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
ALTER TABLE [dbo].[RentHistory] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [Rental_End]
GO
ALTER TABLE [dbo].[RentHistory] ADD  DEFAULT (N'') FOR [Rental_Status]
GO
ALTER TABLE [dbo].[BookExemplar]  WITH CHECK ADD  CONSTRAINT [FK_BookExemplar_Books] FOREIGN KEY([Book_Id])
REFERENCES [dbo].[Books] ([Id_Book])
GO
ALTER TABLE [dbo].[BookExemplar] CHECK CONSTRAINT [FK_BookExemplar_Books]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Genre] FOREIGN KEY([Id_Genre])
REFERENCES [dbo].[Genre] ([Id_Genre])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Genre]
GO
ALTER TABLE [dbo].[Readers]  WITH CHECK ADD  CONSTRAINT [FK_Readers_Roles] FOREIGN KEY([Id_Role])
REFERENCES [dbo].[Roles] ([Id_Role])
GO
ALTER TABLE [dbo].[Readers] CHECK CONSTRAINT [FK_Readers_Roles]
GO
ALTER TABLE [dbo].[RentHistory]  WITH CHECK ADD  CONSTRAINT [FK_RentHistory_Books] FOREIGN KEY([Id_Book])
REFERENCES [dbo].[Books] ([Id_Book])
GO
ALTER TABLE [dbo].[RentHistory] CHECK CONSTRAINT [FK_RentHistory_Books]
GO
ALTER TABLE [dbo].[RentHistory]  WITH CHECK ADD  CONSTRAINT [FK_RentHistory_Readers] FOREIGN KEY([Id_Reader])
REFERENCES [dbo].[Readers] ([Id_User])
GO
ALTER TABLE [dbo].[RentHistory] CHECK CONSTRAINT [FK_RentHistory_Readers]
GO
