CREATE OR ALTER PROCEDURE SP_UPD_FileLogToFailedByFileId
(
    @FileId UNIQUEIDENTIFIER,
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
        Files.Id = @FileId;

    SELECT @@ROWCOUNT 'AffectedRows';
END;