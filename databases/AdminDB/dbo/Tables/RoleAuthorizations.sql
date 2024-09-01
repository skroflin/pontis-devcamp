CREATE TABLE [dbo].[RoleAuthorizations] (
    [RoleId]          INT NOT NULL,
    [AuthorizationId] INT NOT NULL,
    CONSTRAINT [FK_AuthorizationId] FOREIGN KEY ([AuthorizationId]) REFERENCES [dbo].[Authorizations] ([Id]),
    CONSTRAINT [FK_RoleAuthorizations_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);

