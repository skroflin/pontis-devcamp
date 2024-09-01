create procedure dbo.crud_DeleteApplications
	@id int
as
begin
	set nocount on;
	delete from dbo.Applications
	where Id = @id;
end;