CREATE TABLE [dbo].[Countries] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [CountryCode]  CHAR (3)     NULL,
    [Name]         VARCHAR (50) NOT NULL,
    [UserCreated]  VARCHAR (20) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [UserModified] VARCHAR (20) NULL,
    [DateModified] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
create trigger trg_Countries_History
on dbo.Countries
after update, delete
as
begin
	if exists (select 1 from inserted)
	begin
		insert into dbo.Countries_History
		select
			*,
			'U'
		from inserted;
	end

	if exists (select 1 from deleted)
	begin
		insert into dbo.Countries_History
		select
			*,
			'D'
		from deleted;
	end
end;