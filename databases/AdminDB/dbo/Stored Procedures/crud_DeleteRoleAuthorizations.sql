create procedure dbo.crud_DeleteRoleAuthorizations
	@roleId int,
	@authorizationId int
as
begin
	set nocount on;
	delete from dbo.RoleAuthorizations
	where RoleId = @roleId and AuthorizationId = @authorizationId;
end;