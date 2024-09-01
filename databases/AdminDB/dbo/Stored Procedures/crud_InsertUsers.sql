create procedure dbo.crud_InsertUsers
	@username nvarchar(255),
	@email nvarchar(255),
	@password nvarchar(255),
	@isActive bit,
	@isRegistered bit,
	@userCreated nvarchar(max),
	@dateCreated datetime,
	@userModified nvarchar(255),
	@dateModified datetime
as
begin
	set nocount on;
	insert into dbo.Users
	(Username, Email, Password, IsActive, IsRegistered, UserCreated, DateCreated, UserModified, DateModified)
	values
	(@username, @email, @password, @isActive, @isRegistered, @userCreated, @dateCreated, @userModified, @dateModified)

	select scope_identity() as NewId;
end;