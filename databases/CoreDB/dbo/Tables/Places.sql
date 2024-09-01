CREATE TABLE [dbo].[Places] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [PlaceNationalCode] VARCHAR (20)  NULL,
    [Name]              VARCHAR (255) NOT NULL,
    [DistrictId]        INT           NOT NULL,
    [RegionId]          INT           NOT NULL,
    [UserCreated]       VARCHAR (20)  NOT NULL,
    [DateCreated]       DATETIME      NULL,
    [UserModified]      VARCHAR (20)  NULL,
    [DateModified]      DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Places_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [dbo].[Districts] ([Id]),
    CONSTRAINT [FK_Places_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Regions] ([Id])
);


GO
create trigger trg_Places_History
on dbo.Places
after update, delete
as
begin
	if exists (select 1 from inserted)
	begin
		insert into dbo.Places_History
		select
			*,
			'U'
		from inserted;
	end

	if exists (select 1 from deleted)
	begin
		insert into dbo.Places_History
		select
			*,
			'D'
		from deleted;
	end
end;