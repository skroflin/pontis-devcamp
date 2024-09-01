create procedure dbo.crud_SelectUsers
	@Id int = null
as
begin
	set nocount on;

	if @Id is not null
	begin
		select
		*
		from dbo.Users
		where Id = @Id;
	end
	else
	begin
		select
		*
		from dbo.Users;
	end
end;