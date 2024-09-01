create function dbo.fn_GetDayType
(
	@date datetime
)
returns nvarchar(50)
as
begin
	declare @dayType nvarchar(50);

	if datename(weekday, @date) in ('Saturday', 'Sunday')
	begin
		set @dayType = 'Weekend';
	end
	begin
		set @dayType = 'Weekday';
	end
	
	return @dayType;
end;