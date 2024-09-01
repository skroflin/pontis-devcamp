CREATE TABLE [dbo].[Applications_History] (
    [Id]           INT          NULL,
    [Name]         VARCHAR (20) NULL,
    [UserCreated]  VARCHAR (20) NULL,
    [DateCreated]  DATETIME     NULL,
    [UserModified] VARCHAR (20) NULL,
    [DateModified] DATETIME     NULL,
    [ChangeType]   CHAR (1)     NOT NULL
);

