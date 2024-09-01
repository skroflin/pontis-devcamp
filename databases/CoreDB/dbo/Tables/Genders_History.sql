CREATE TABLE [dbo].[Genders_History] (
    [Id]           INT          NULL,
    [GenderShort]  CHAR (1)     NULL,
    [Name]         VARCHAR (20) NULL,
    [UserCreated]  VARCHAR (20) NULL,
    [DateCreated]  DATETIME     NULL,
    [UserModified] VARCHAR (20) NULL,
    [DateModified] DATETIME     NULL,
    [ChangeType]   CHAR (1)     NOT NULL
);

