CREATE TABLE [dbo].[Users_History] (
    [Id]           INT           NULL,
    [Username]     VARCHAR (100) NULL,
    [Email]        VARCHAR (255) NULL,
    [Password]     VARCHAR (200) NULL,
    [IsActive]     BIT           NULL,
    [IsRegistered] BIT           NULL,
    [UserCreated]  VARCHAR (20)  NULL,
    [DateCreated]  DATETIME      NULL,
    [UserModified] VARCHAR (20)  NULL,
    [DateModified] DATETIME      NULL,
    [ChangeType]   CHAR (1)      NOT NULL
);

