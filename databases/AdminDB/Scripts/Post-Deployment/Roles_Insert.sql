USE [AdminDB]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (1, N'Admin', N'admin', CAST(N'2024-08-22T08:22:58.280' AS DateTime), NULL, NULL)
INSERT [dbo].[Roles] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (2, N'Reader', N'admin', CAST(N'2024-08-22T08:22:58.280' AS DateTime), NULL, NULL)
INSERT [dbo].[Roles] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (3, N'Writer', N'admin', CAST(N'2024-08-22T08:22:58.280' AS DateTime), NULL, NULL)
INSERT [dbo].[Roles] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (4, N'Guest', N'admin', CAST(N'2024-08-22T08:22:58.280' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Roles] OFF