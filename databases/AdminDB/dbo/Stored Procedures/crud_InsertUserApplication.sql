create procedure dbo.crud_InsertUserApplication
	@userId int,
	@applicationId int,
	@roleId int
as
begin
	set nocount on;
	insert into dbo.UserApplication
	(UserId, ApplicationId, RoleId)
	values
	(@userId, @applicationId, @roleId);
end;