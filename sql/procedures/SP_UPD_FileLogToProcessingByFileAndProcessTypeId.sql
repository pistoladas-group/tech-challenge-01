CREATE OR ALTER PROCEDURE SP_UPD_FileLogToProcessingByFileIdAndProcessTypeId
(
    @FileId UNIQUEIDENTIFIER,
    @ProcessTypeId TINYINT,
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
        Files.Id = @FileId AND
        FileLogs.ProcessTypeId = @ProcessTypeId;

    SELECT @@ROWCOUNT 'AffectedRows';
END;