CREATE OR ALTER PROCEDURE SP_LST_Files
(
	@PageNumber BIGINT,
	@PageSize BIGINT
)
AS
BEGIN
    SELECT
        Files.Id,
        Files.IsDeleted,
        Files.CreatedAt,
        Files.Name,
        Files.Extension,
        Files.SizeInBytes,
        Files.Url,
        Files.ProcessStatusId
    FROM
        Files
    ORDER BY
        Files.Id
    OFFSET
        (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT
        @PageSize ROWS ONLY;
END;