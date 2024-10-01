create procedure dbo.crud_DeleteAuthorizations
	@id int
as
begin
	set nocount on;
	delete from dbo.Authorizations
	where Id = @id;
end;