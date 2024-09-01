CREATE TABLE [dbo].[Employees_History] (
    [Id]               INT           NULL,
    [Username]         VARCHAR (20)  NULL,
    [Firstname]        VARCHAR (100) NULL,
    [Lastname]         VARCHAR (100) NULL,
    [NationalIdNumber] VARCHAR (20)  NULL,
    [NationalIdType]   INT           NULL,
    [GenderId]         INT           NULL,
    [Birthdate]        DATE          NULL,
    [Address]          VARCHAR (100) NULL,
    [PlaceId]          INT           NULL,
    [CountryId]        INT           NULL,
    [UserCreated]      VARCHAR (20)  NULL,
    [DateCreated]      DATETIME      NULL,
    [UserModified]     VARCHAR (20)  NULL,
    [DateModified]     DATETIME      NULL,
    [ChangeType]       CHAR (1)      NOT NULL
);

