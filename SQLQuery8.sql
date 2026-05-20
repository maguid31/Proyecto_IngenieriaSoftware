USE [proyecto_ingenieria]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 19/5/2026 19:23:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[Modulo] [nvarchar](50) NOT NULL,
	[Accion] [nvarchar](100) NOT NULL,
	[Criticidad] [int] NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Idiomas]    Script Date: 19/5/2026 19:23:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idiomas](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Perfiles]    Script Date: 19/5/2026 19:23:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfiles](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 19/5/2026 19:23:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[DNI] [nvarchar](20) NOT NULL,
	[Contraseña] [nvarchar](200) NOT NULL,
	[PerfilId] [int] NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Activo] [bit] NOT NULL,
	[IntentosFallidos] [int] NOT NULL,
	[PrimerLogin] [bit] NOT NULL,
	[Nombre] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bitacora] ON 

INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (1, 1, CAST(N'2026-05-19T10:54:37.350' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (2, 1, CAST(N'2026-05-19T10:54:54.357' AS DateTime), N'Usuarios', N'Cambio Contraseña', 2, N'El usuario cambió su contraseña')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (3, 1, CAST(N'2026-05-19T10:55:01.897' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (4, 1, CAST(N'2026-05-19T10:55:06.260' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (5, 1, CAST(N'2026-05-19T10:55:19.213' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (6, 1, CAST(N'2026-05-19T10:58:57.540' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (7, 1, CAST(N'2026-05-19T10:59:47.140' AS DateTime), N'Usuarios', N'Alta Usuario', 3, N'Se registró un nuevo usuario: dargenzio42076585')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (8, 1, CAST(N'2026-05-19T11:00:14.587' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión y salió del sistema')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (9, 2, CAST(N'2026-05-19T11:00:31.573' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (10, 2, CAST(N'2026-05-19T11:00:43.437' AS DateTime), N'Usuarios', N'Cambio Contraseña', 2, N'El usuario cambió su contraseña')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (11, 2, CAST(N'2026-05-19T11:00:48.083' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (12, 2, CAST(N'2026-05-19T11:01:46.870' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (13, 2, CAST(N'2026-05-19T11:02:02.120' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (14, 2, CAST(N'2026-05-19T11:02:51.610' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (15, 2, CAST(N'2026-05-19T16:49:01.700' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (16, 2, CAST(N'2026-05-19T16:50:16.197' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (17, 2, CAST(N'2026-05-19T17:04:07.030' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (18, 2, CAST(N'2026-05-19T17:04:53.277' AS DateTime), N'Usuarios', N'Alta Usuario', 3, N'Se registró un nuevo usuario: Diaz12345678')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (19, 2, CAST(N'2026-05-19T17:06:20.010' AS DateTime), N'Usuarios', N'Alta Usuario', 3, N'Se registró un nuevo usuario: Rivero47293916')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (20, 2, CAST(N'2026-05-19T17:07:03.223' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión y salió del sistema')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (21, 2, CAST(N'2026-05-19T17:13:32.403' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (22, 2, CAST(N'2026-05-19T17:15:30.190' AS DateTime), N'Usuarios', N'Alta Usuario', 3, N'Se registró un nuevo usuario: Lopez12345677')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (23, 2, CAST(N'2026-05-19T17:15:46.257' AS DateTime), N'Usuarios', N'Modificar Usuario', 2, N'Se modificó al usuario: Lopez12345677')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (24, 2, CAST(N'2026-05-19T17:16:05.807' AS DateTime), N'Usuarios', N'Bloquear Usuario', 3, N'Se deshabilitó al usuario: Lopez12345677')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (25, 2, CAST(N'2026-05-19T17:16:23.313' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión y salió del sistema')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (26, 2, CAST(N'2026-05-19T17:18:07.160' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (27, 2, CAST(N'2026-05-19T17:18:20.990' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión y salió del sistema')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (28, 3, CAST(N'2026-05-19T17:18:42.817' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (29, 3, CAST(N'2026-05-19T17:19:09.513' AS DateTime), N'Usuarios', N'Cambio Contraseña', 2, N'El usuario cambió su contraseña')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (30, 3, CAST(N'2026-05-19T17:19:35.333' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (31, 3, CAST(N'2026-05-19T17:19:39.380' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (32, 3, CAST(N'2026-05-19T17:31:36.167' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (33, 3, CAST(N'2026-05-19T17:31:38.943' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (34, 3, CAST(N'2026-05-19T17:32:01.997' AS DateTime), N'Usuarios', N'Bloqueo por Intentos', 4, N'Cuenta bloqueada por superar intentos fallidos')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (35, 2, CAST(N'2026-05-19T17:32:36.820' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (36, 2, CAST(N'2026-05-19T17:34:29.270' AS DateTime), N'Usuarios', N'Modificar Usuario', 2, N'Se habilitó al usuario: Diaz12345678')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (37, 2, CAST(N'2026-05-19T17:34:36.007' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión y salió del sistema')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (38, 2, CAST(N'2026-05-19T17:34:55.027' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (39, 2, CAST(N'2026-05-19T17:35:42.840' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (40, 2, CAST(N'2026-05-19T17:36:47.177' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (41, 2, CAST(N'2026-05-19T17:39:42.703' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (42, 2, CAST(N'2026-05-19T17:41:41.760' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (43, 2, CAST(N'2026-05-19T17:43:58.013' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (44, 2, CAST(N'2026-05-19T19:03:06.977' AS DateTime), N'Usuarios', N'Login', 1, N'Usuario inició sesión')
INSERT [dbo].[Bitacora] ([Id], [UsuarioId], [FechaHora], [Modulo], [Accion], [Criticidad], [Descripcion]) VALUES (45, 2, CAST(N'2026-05-19T19:03:18.940' AS DateTime), N'Usuarios', N'Logout', 1, N'El usuario cerró sesión desde el menú principal')
SET IDENTITY_INSERT [dbo].[Bitacora] OFF
GO
INSERT [dbo].[Idiomas] ([Id], [Nombre]) VALUES (1, N'Español')
INSERT [dbo].[Idiomas] ([Id], [Nombre]) VALUES (2, N'Inglés')
GO
INSERT [dbo].[Perfiles] ([Id], [Nombre], [Descripcion]) VALUES (1, N'Administrador', N'Acceso total al sistema')
INSERT [dbo].[Perfiles] ([Id], [Nombre], [Descripcion]) VALUES (2, N'Basico', N'Acceso estandar para usuarios')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [Apellido], [DNI], [Contraseña], [PerfilId], [Email], [Activo], [IntentosFallidos], [PrimerLogin], [Nombre]) VALUES (1, N'Sistema', N'1234', N'f5517ce0685d53dacb4857d15b356aa6c0bbe23017eaca1ede9bb7bc71567f63', 1, NULL, 1, 0, 0, N'Admin')
INSERT [dbo].[Usuarios] ([Id], [Apellido], [DNI], [Contraseña], [PerfilId], [Email], [Activo], [IntentosFallidos], [PrimerLogin], [Nombre]) VALUES (2, N'dargenzio', N'42076585', N'ff3e1481ce7018d099894887b2c026589a4a921ce85982f1e46c4d0586656de5', 1, N'magali@gmail.com', 1, 0, 0, N'magali')
INSERT [dbo].[Usuarios] ([Id], [Apellido], [DNI], [Contraseña], [PerfilId], [Email], [Activo], [IntentosFallidos], [PrimerLogin], [Nombre]) VALUES (3, N'Diaz', N'12345678', N'8ac2596dff8b9d561fc378a424bb14e0eb97c2608eb2d12ca20559110f214f1a', 2, N'diaz@gmail.com', 1, 0, 0, N'Diego')
INSERT [dbo].[Usuarios] ([Id], [Apellido], [DNI], [Contraseña], [PerfilId], [Email], [Activo], [IntentosFallidos], [PrimerLogin], [Nombre]) VALUES (4, N'Rivero', N'47293916', N'33576f386973daf6bc5bbb9c9874876880aa364ea58f9b5acf1a155ccab19e51', 1, N'rivero@gmail.com', 1, 0, 1, N'Milagros')
INSERT [dbo].[Usuarios] ([Id], [Apellido], [DNI], [Contraseña], [PerfilId], [Email], [Activo], [IntentosFallidos], [PrimerLogin], [Nombre]) VALUES (5, N'Lopez', N'12345677', N'79c24954807c83c719a99fa71958ad1afb7b17a2fe03d4773d383797dfcbfbf3', 2, N'lopezmaria@gmail.com', 0, 0, 1, N'Maria')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_DNI]    Script Date: 19/5/2026 19:23:25 ******/
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [UQ_DNI] UNIQUE NONCLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bitacora] ADD  DEFAULT (getdate()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [IntentosFallidos]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((1)) FOR [PrimerLogin]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Usuarios]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Perfiles] FOREIGN KEY([PerfilId])
REFERENCES [dbo].[Perfiles] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Perfiles]
GO
