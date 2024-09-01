USE [CoreDB]

GO
SET IDENTITY_INSERT [dbo].[Genders] ON 

INSERT [dbo].[Genders] ([Id], [GenderShort], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (1, N'M', N'Male', N'admin', CAST(N'2024-08-24T23:21:56.680' AS DateTime), NULL, NULL)
INSERT [dbo].[Genders] ([Id], [GenderShort], [Name], [UserCreated], [DateCreated], [UserModified], [DateModified]) VALUES (2, N'F', N'Female', N'admin', CAST(N'2024-08-24T23:21:56.680' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Genders] OFF