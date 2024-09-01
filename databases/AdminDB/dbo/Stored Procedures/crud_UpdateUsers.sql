create procedure dbo.crud_UpdateUsers
	@id int,
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
	update dbo.Users
	set
	Username = @username,
	Email = @email,
	Password = @password,
	IsActive = @isActive,
	IsRegistered = @isRegistered,
	UserCreated = @userCreated,
	DateCreated = @dateCreated,
	UserModified = @userModified,
	DateModified = @dateModified
	where
	Id = @id;
end;