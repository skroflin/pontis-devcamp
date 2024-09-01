CREATE TABLE [dbo].[Countries_History] (
    [Id]           INT          NULL,
    [CountryCode]  CHAR (3)     NULL,
    [Name]         VARCHAR (50) NULL,
    [UserCreated]  VARCHAR (20) NULL,
    [DateCreated]  DATETIME     NULL,
    [UserModified] VARCHAR (20) NULL,
    [DateModified] DATETIME     NULL,
    [ChangeType]   CHAR (1)     NOT NULL
);

