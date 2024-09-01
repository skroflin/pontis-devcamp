CREATE TABLE [dbo].[UserApplication] (
    [UserId]        INT NOT NULL,
    [ApplicationId] INT NOT NULL,
    [RoleId]        INT NOT NULL,
    CONSTRAINT [FK_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Applications] ([Id]),
    CONSTRAINT [FK_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]),
    CONSTRAINT [FK_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

