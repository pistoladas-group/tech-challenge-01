CREATE OR ALTER PROCEDURE SP_UPD_FileProcessStatusById
(
    @FileId UNIQUEIDENTIFIER,
    @ProcessStatusId TINYINT
)
AS
BEGIN
    UPDATE 
        Files
    SET
        Files.ProcessStatusId = @ProcessStatusId
    WHERE
        Files.Id = @FileId;

    SELECT @@ROWCOUNT 'AffectedRows';
END;