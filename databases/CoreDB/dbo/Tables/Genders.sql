CREATE TABLE [dbo].[Genders] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [GenderShort]  CHAR (1)     NOT NULL,
    [Name]         VARCHAR (20) NOT NULL,
    [UserCreated]  VARCHAR (20) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [UserModified] VARCHAR (20) NULL,
    [DateModified] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
create trigger trg_Genders_History
on dbo.Genders
after update, delete
as
begin
	if exists (select 1 from inserted)
	begin
		insert into dbo.Genders_History
		select
			*,
			'U'
		from inserted;
	end

	if exists (select 1 from deleted)
	begin
		insert into dbo.Genders_History
		select
			*,
			'D'
		from deleted;
	end
end;