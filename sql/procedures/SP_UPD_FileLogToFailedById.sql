CREATE OR ALTER PROCEDURE SP_UPD_FileLogToFailedById
(
    @Id UNIQUEIDENTIFIER,
    @ProcessStatusId TINYINT,
    @FinishedAt DATETIME,
    @ErrorMessage VARCHAR(1000)
)
AS
BEGIN
    UPDATE 
        FileLogs
    SET
        ProcessStatusId = @ProcessStatusId,
        FinishedAt = @FinishedAt,
        ErrorMessage = @ErrorMessage
    WHERE
        FileLogs.Id = @Id;

    SELECT @@ROWCOUNT 'AffectedRows';
END;