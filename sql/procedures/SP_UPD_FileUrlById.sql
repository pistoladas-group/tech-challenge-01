CREATE OR ALTER PROCEDURE SP_UPD_FileUrlById
(
    @Id UNIQUEIDENTIFIER,
    @Url VARCHAR(2500)
)
AS
BEGIN
    UPDATE 
        Files
    SET
        Files.Url = @Url
    WHERE
        Files.Id = @Id;

    SELECT @@ROWCOUNT 'AffectedRows';
END;