CREATE PROCEDURE dbo.crud_SelectAuthorizations
    @Id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Id IS NOT NULL
    BEGIN
        SELECT Id, Name, UserCreated, DateCreated
        FROM dbo.Authorizations
        WHERE Id = @Id;
    END
    ELSE
    BEGIN
        SELECT Id, Name, UserCreated, DateCreated
        FROM dbo.Authorizations;
    END
END;
