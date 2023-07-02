CREATE OR ALTER PROCEDURE SP_UPD_FileUrlById
(
    @FileId UNIQUEIDENTIFIER,
    @Url VARCHAR(2500) = NULL
)
AS
BEGIN
    UPDATE 
        Files
    SET
        Files.ProcessStatusId = 4,
        Files.Url = @Url
    WHERE
        Files.Id = @FileId;

    SELECT @@ROWCOUNT 'AffectedRows';
END;