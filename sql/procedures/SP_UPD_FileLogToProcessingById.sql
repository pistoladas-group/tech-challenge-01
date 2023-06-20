CREATE OR ALTER PROCEDURE SP_UPD_FileLogToProcessingById
(
    @Id UNIQUEIDENTIFIER,
    @ProcessStatusId TINYINT,
    @StartedAt DATETIME
)
AS
BEGIN
    UPDATE 
        FileLogs
    SET
        ProcessStatusId = @ProcessStatusId,
        StartedAt = @StartedAt
    WHERE
        FileLogs.Id = @Id;

    SELECT @@ROWCOUNT 'AffectedRows';
END;