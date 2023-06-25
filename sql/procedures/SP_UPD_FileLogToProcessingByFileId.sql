CREATE OR ALTER PROCEDURE SP_UPD_FileLogToProcessingByFileId
(
    @FileId UNIQUEIDENTIFIER,
    @StartedAt DATETIME
)
AS
BEGIN
    UPDATE 
        FileLogs
    SET
        FileLogs.ProcessStatusId = 2, --Processing
        FileLogs.StartedAt = @StartedAt
    FROM
        FileLogs
    INNER JOIN
        Files ON FileLogs.FileId = Files.Id
    WHERE
        Files.Id = @FileId;

    SELECT @@ROWCOUNT 'AffectedRows';
END;