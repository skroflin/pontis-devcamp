CREATE TABLE [dbo].[Users] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Username]     VARCHAR (100) NOT NULL,
    [Email]        VARCHAR (255) NOT NULL,
    [Password]     VARCHAR (200) NOT NULL,
    [IsActive]     BIT           NOT NULL,
    [IsRegistered] BIT           NOT NULL,
    [UserCreated]  VARCHAR (20)  NOT NULL,
    [DateCreated]  DATETIME      NOT NULL,
    [UserModified] VARCHAR (20)  NULL,
    [DateModified] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);


GO
create trigger trg_Users_History
on dbo.Users
after update, delete
as
begin
	if exists (select 1 from inserted)
	begin
		insert into dbo.Users_History
		select
			*,
			'U'
		from inserted;
	end

	if exists (select 1 from deleted)
	begin
		insert into dbo.Users_History
		select
			*,
			'D'
		from deleted;
	end
end;