create function dbo.fn_GetUserApplications
(
	@userId int
)
returns table
as
return
(
	select
		u.Username as Username,
		u.Email as UserEmail,
		r.Name as Role,
		a.Name as AppName
	from
		dbo.UserApplication ua
		inner join dbo.Roles r on ua.RoleId = r.Id
		inner join dbo.Applications a on ua.ApplicationId = a.Id
		inner join dbo.Users u on ua.UserId = u.Username
	where
	ua.UserId = @userId
);