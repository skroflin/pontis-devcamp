CREATE TABLE [dbo].[Regions_History] (
    [Id]           INT          NULL,
    [Name]         VARCHAR (50) NULL,
    [CountryId]    INT          NULL,
    [UserCreated]  VARCHAR (20) NULL,
    [DateCreated]  DATETIME     NULL,
    [UserModified] VARCHAR (20) NULL,
    [DateModified] DATETIME     NULL,
    [ChangeType]   CHAR (1)     NOT NULL
);

