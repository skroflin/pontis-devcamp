declare @cutOffDateTime datetime;

SET @CutoffDateTime = DATEADD(HOUR, -1, DATEADD(YEAR, -2, GETDATE()));

print 'Cutoff DateTime is: ' + convert(nvarchar, @cutOffDateTime, 120);

select count(*)
from dbo.Logs
where LogTimeStamp < @cutOffDateTime;

insert into dbo.Logs_History
(Id, Name, MessageText, MessageType, UserCreated, LogTimeStamp, DateMoved)
select Id, Name, MessageText, MessageType, UserCreated, LogTimeStamp, getdate()
from dbo.Logs
where LogTimeStamp < @cutOffDateTime;

delete from dbo.Logs
where LogTimeStamp < @cutOffDateTime;

print 'Transfer from dbo.Logs to dbo.Logs_History completed for records older than '
+ convert(nvarchar, @cutOffDateTime, 120);