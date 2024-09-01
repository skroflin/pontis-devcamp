CREATE TABLE [dbo].[Districts] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [RegionId]     INT          NOT NULL,
    [Name]         VARCHAR (50) NOT NULL,
    [DistrictType] VARCHAR (20) NOT NULL,
    [UserCreated]  VARCHAR (20) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [UserModified] VARCHAR (20) NULL,
    [DateModified] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Districts_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Regions] ([Id])
);


GO
create trigger trg_Districts_History
on dbo.Districts
after update, delete
as
begin
	if exists (select 1 from inserted)
	begin
		insert into dbo.Districts_History
		select
			*,
			'U'
		from inserted;
	end

	if exists (select 1 from deleted)
	begin
		insert into dbo.Districts_History
		select
			*,
			'D'
		from deleted;
	end
end;