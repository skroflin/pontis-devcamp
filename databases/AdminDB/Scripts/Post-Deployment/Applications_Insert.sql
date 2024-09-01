USE [AdminDB]
GO
SET IDENTITY_INSERT [dbo].[Applications] ON 

INSERT [dbo].[Applications] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (2, N'ConsoleApp', N'admin', CAST(N'2024-08-21T21:29:16.187' AS DateTime), NULL, NULL)
INSERT [dbo].[Applications] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (3, N'WpfApp', N'admin', CAST(N'2024-08-21T21:29:16.187' AS DateTime), NULL, NULL)
INSERT [dbo].[Applications] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (4, N'NetCoreApi', N'admin', CAST(N'2024-08-21T21:29:16.187' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Applications] OFF