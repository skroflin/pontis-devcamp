create function dbo.fn_GetRoleAuthorizations
(
	@roleId int
)
returns table
as
return
(
	select AuthorizationId
	from dbo.RoleAuthorizations
	where
	@roleId = RoleId
);