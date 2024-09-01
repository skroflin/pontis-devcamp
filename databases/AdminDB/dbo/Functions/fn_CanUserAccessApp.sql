create function dbo.fn_CanUserAccessApp
(
	@userId int,
	@applicationId int
)
returns bit
as
begin
	declare @canAccess bit;

	if exists(
		select 1
		from dbo.UserApplication
		where 
		UserId = @userId 
		and
		ApplicationId = @applicationId
	)
	begin
		set @canAccess = 1;
	end
	else
	begin
		set @canAccess = 0;
	end

	return @canAccess;
end;