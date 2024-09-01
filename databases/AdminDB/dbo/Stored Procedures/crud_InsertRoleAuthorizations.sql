create procedure dbo.crud_InsertRoleAuthorizations
	@roleId int,
	@authorizationId int
as
begin
	set nocount on;
	insert into dbo.RoleAuthorizations
	(RoleId, AuthorizationId)
	values
	(@roleId, @authorizationId);
end;