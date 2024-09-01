﻿CREATE TABLE [dbo].[Roles] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (20) NOT NULL,
    [UserCreated]  VARCHAR (20) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [UserModified] VARCHAR (20) NULL,
    [DateModified] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
create trigger trg_Roles_History
on dbo.Roles
after update, delete
as
begin
	if exists (select 1 from inserted)
	begin
		insert into dbo.Roles_History
		select
			*,
			'U'
		from inserted;
	end

	if exists (select 1 from deleted)
	begin
		insert into dbo.Roles_History
		select
			*,
			'D'
		from deleted;
	end
end;