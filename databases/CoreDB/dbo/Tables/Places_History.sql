CREATE TABLE [dbo].[Places_History] (
    [Id]                INT           NULL,
    [PlaceNationalCode] VARCHAR (20)  NULL,
    [Name]              VARCHAR (255) NULL,
    [DistrictId]        INT           NULL,
    [RegionId]          INT           NULL,
    [UserCreated]       VARCHAR (20)  NULL,
    [DateCreated]       DATETIME      NULL,
    [UserModified]      VARCHAR (20)  NULL,
    [DateModified]      DATETIME      NULL,
    [ChangeType]        CHAR (1)      NOT NULL
);

