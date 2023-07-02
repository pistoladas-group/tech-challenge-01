CREATE OR ALTER PROCEDURE SP_LST_PendingFiles
(
	@PageNumber BIGINT,
	@PageSize BIGINT
)
AS
BEGIN
    SELECT DISTINCT
        Files.Id,
        Files.CreatedAt
    FROM
        FileLogs
    INNER JOIN
        Files on FileLogs.FileId = Files.Id
    WHERE
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
        Files.CreatedAt
    OFFSET
        (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT
        @PageSize ROWS ONLY;
END;