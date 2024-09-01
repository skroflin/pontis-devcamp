CREATE TABLE [dbo].[Employees] (
    [Id]               INT           NOT NULL,
    [Username]         VARCHAR (20)  NOT NULL,
    [Firstname]        VARCHAR (100) NOT NULL,
    [Lastname]         VARCHAR (100) NOT NULL,
    [NationalIdNumber] VARCHAR (20)  NOT NULL,
    [NationalIdType]   INT           NOT NULL,
    [GenderId]         INT           NOT NULL,
    [Birthdate]        DATE          NULL,
    [Address]          VARCHAR (100) NULL,
    [PlaceId]          INT           NOT NULL,
    [CountryId]        INT           NOT NULL,
    [UserCreated]      VARCHAR (20)  NOT NULL,
    [DateCreated]      DATETIME      NOT NULL,
    [UserModified]     VARCHAR (20)  NULL,
    [DateModified]     DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employees_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id]),
    CONSTRAINT [FK_Employees_GenderId] FOREIGN KEY ([GenderId]) REFERENCES [dbo].[Genders] ([Id]),
    CONSTRAINT [FK_Employees_NationalIdType] FOREIGN KEY ([NationalIdType]) REFERENCES [dbo].[NationalIdTypes] ([Id]),
    CONSTRAINT [FK_Employees_PlaceId] FOREIGN KEY ([PlaceId]) REFERENCES [dbo].[Places] ([Id])
);


GO
create trigger trg_Employees_History
on dbo.Employees
after update, delete
as
begin
	if exists (select 1 from inserted)
	begin
		insert into dbo.Employees_History
		select
			*,
			'U'
		from inserted;
	end

	if exists (select 1 from deleted)
	begin
		insert into dbo.Employees_History
		select
			*,
			'D'
		from deleted;
	end
end;