CREATE OR ALTER PROCEDURE SP_LST_FilePendingLogs
(
    @FileId UNIQUEIDENTIFIER,
	@PageNumber BIGINT,
	@PageSize BIGINT
)
AS
BEGIN
    SELECT
        FileLogs.FileId,
        Files.Name 'FileName',
        Files.ContentType 'FileContentType',
        FileLogs.ProcessTypeId 
    FROM
        FileLogs
    INNER JOIN
        Files on FileLogs.FileId = Files.Id
    WHERE
        FileLogs.ProcessStatusId = 1 and
        FileLogs.IsDeleted = 0 and
        Files.IsDeleted = 0 and
        files.Id = @FileId
    ORDER BY
        Files.Id
    OFFSET
        (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT
        @PageSize ROWS ONLY;
END;