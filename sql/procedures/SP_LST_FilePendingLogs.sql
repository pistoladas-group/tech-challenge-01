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
        FileLogs.FileId = @FileId AND
        FileLogs.ProcessStatusId = 1 AND
        FileLogs.IsDeleted = 0 AND
        (   
            Files.IsDeleted = 0 OR 
            (
                Files.IsDeleted = 1 AND
                FileLogs.ProcessTypeId = 2 -- Delete
            ) 
        )
    ORDER BY
        Files.Id
    OFFSET
        (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT
        @PageSize ROWS ONLY;
END;