create procedure dbo.crud_InsertAuthorizations
	@name nvarchar(255),
	@userCreated nvarchar(255),
	@dateCreated datetime,
	@userModified nvarchar(255),
	@dateModified datetime
as
begin
	set nocount on;
	insert into dbo.Authorizations
	(Name, UserCreated, DateCreated, UserModified, DateModified)
	values
	(@name, @userCreated, @dateCreated, @userModified, @dateModified);
	select scope_identity() as NewId;
end;