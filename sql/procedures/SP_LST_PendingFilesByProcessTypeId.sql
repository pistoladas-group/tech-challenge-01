CREATE OR ALTER PROCEDURE SP_LST_PendingFiles
(
	@PageNumber BIGINT,
	@PageSize BIGINT
)
AS
BEGIN
    SELECT DISTINCT
        Files.Id
    FROM
        FileLogs
    INNER JOIN
        Files on FileLogs.FileId = Files.Id
    WHERE
        FileLogs.ProcessStatusId = 1 and
        FileLogs.IsDeleted = 0 and
        Files.IsDeleted = 0
    ORDER BY
        Files.Id
    OFFSET
        (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT
        @PageSize ROWS ONLY;
END;