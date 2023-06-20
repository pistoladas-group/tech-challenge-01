CREATE OR ALTER PROCEDURE SP_GET_FileLogById
(
	@Id UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT
        FileLogs.Id,
        FileLogs.IsDeleted,
        FileLogs.CreatedAt,
        FileLogs.FileId,
        FileLogs.ProcessStatusId,
        FileLogs.ProcessTypeId,
        FileLogs.ErrorMessage,
        FileLogs.StartedAt,
        FileLogs.FinishedAt
    FROM
        FileLogs
    WHERE
        FileLogs.IsDeleted = 0 AND
        FileLogs.Id = @Id
END;