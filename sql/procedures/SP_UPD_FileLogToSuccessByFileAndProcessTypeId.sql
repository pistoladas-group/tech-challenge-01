CREATE OR ALTER PROCEDURE SP_UPD_FileLogToSuccessByFileAndProcessTypeId
(
    @FileId UNIQUEIDENTIFIER,
    @ProcessTypeId TINYINT,
    @FinishedAt DATETIME
)
AS
BEGIN
    UPDATE 
        FileLogs
    SET
        FileLogs.ProcessStatusId = 4, --Success
        FileLogs.FinishedAt = @FinishedAt
    FROM
        FileLogs
    INNER JOIN
        Files ON FileLogs.FileId = Files.Id
    WHERE
        Files.Id = @FileId AND
        Files.IsDeleted = 0 AND
        FileLogs.ProcessTypeId = @ProcessTypeId;

    SELECT @@ROWCOUNT 'AffectedRows';
END;