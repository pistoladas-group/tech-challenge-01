CREATE OR ALTER PROCEDURE SP_UPD_FileLogToSuccessById
(
    @Id UNIQUEIDENTIFIER,
    @ProcessStatusId TINYINT,
    @FinishedAt DATETIME
)
AS
BEGIN
    UPDATE 
        FileLogs
    SET
        ProcessStatusId = @ProcessStatusId,
        FinishedAt = @FinishedAt
    WHERE
        FileLogs.Id = @Id;

    SELECT @@ROWCOUNT 'AffectedRows';
END;