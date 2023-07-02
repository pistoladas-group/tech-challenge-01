CREATE OR ALTER PROCEDURE SP_UPD_FileLogToFailedByFileAndProcessTypeId
(
    @FileId UNIQUEIDENTIFIER,
    @ProcessTypeId TINYINT,
    @FinishedAt DATETIME,
    @ErrorMessage VARCHAR(1000)
)
AS
BEGIN
    UPDATE 
        FileLogs
    SET
        FileLogs.ProcessStatusId = 3, --Failed
        FileLogs.FinishedAt = @FinishedAt,
        FileLogs.ErrorMessage = @ErrorMessage
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