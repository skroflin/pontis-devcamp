create procedure dbo.crud_UpdateApplications
	@id int,
	@name nvarchar(255),
	@userCreated nvarchar(255),
	@dateCreated datetime,
	@userModified nvarchar(255),
	@dateModified datetime
as
begin
	set nocount on;
	update dbo.Applications
	set
	Name = @name,
	UserCreated = @userCreated,
	DateCreated = @dateCreated,
	UserModified = @userModified,
	DateModified = @dateModified
	where
	Id = @id;
end;