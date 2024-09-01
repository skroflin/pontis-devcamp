create procedure dbo.crud_DeleteUsers
	@id int
as
begin
	set nocount on;
	delete from dbo.Users
	where
	Id = @id;
end;