USE [AdminDB]
GO
SET IDENTITY_INSERT [dbo].[Authorizations] ON 

INSERT [dbo].[Authorizations] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (1, N'Name', N'admin', CAST(N'2024-08-22T08:45:11.710' AS DateTime), NULL, NULL)
INSERT [dbo].[Authorizations] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (2, N'Read', N'admin', CAST(N'2024-08-22T08:45:11.710' AS DateTime), NULL, NULL)
INSERT [dbo].[Authorizations] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (3, N'Write', N'admin', CAST(N'2024-08-22T08:45:11.710' AS DateTime), NULL, NULL)
INSERT [dbo].[Authorizations] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (4, N'Save', N'admin', CAST(N'2024-08-22T08:45:11.710' AS DateTime), NULL, NULL)
INSERT [dbo].[Authorizations] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (5, N'Open', N'admin', CAST(N'2024-08-22T08:45:11.710' AS DateTime), NULL, NULL)
INSERT [dbo].[Authorizations] ([Id], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (6, N'Delete', N'admin', CAST(N'2024-08-22T08:45:11.710' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Authorizations] OFF
GO