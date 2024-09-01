create procedure dbo.crud_SelectApplications
	@id int = null
as
begin
	set nocount on;

	if @id is not null
	begin
		select * 
		from dbo.Applications
		where Id = @id;
	end
	else
	begin
		select *
		from dbo.Applications;
	end
end;