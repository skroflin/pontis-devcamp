create procedure dbo.crud_DeleteUserApplication
	@userId int,
	@applicationId int,
	@roleId int
as
begin
	delete from dbo.UserApplication
	where UserId = @userId and ApplicationId = @applicationId and RoleId = @roleId;
end;