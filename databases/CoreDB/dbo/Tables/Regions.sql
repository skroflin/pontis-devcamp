CREATE TABLE [dbo].[Regions] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50) NOT NULL,
    [CountryId]    INT          NOT NULL,
    [UserCreated]  VARCHAR (20) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [UserModified] VARCHAR (20) NULL,
    [DateModified] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Regions_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
);


GO
create trigger trg_Regions_History
on dbo.Regions
after update, delete
as
begin
	if exists (select 1 from inserted)
	begin
		insert into dbo.Regions_History
		select
			*,
			'U'
		from inserted;
	end

	if exists (select 1 from deleted)
	begin
		insert into dbo.Regions_History
		select
			*,
			'D'
		from deleted;
	end
end;