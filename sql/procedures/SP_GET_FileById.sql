CREATE OR ALTER PROCEDURE SP_GET_FileById
(
	@Id UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT
        Files.Id,
        Files.IsDeleted,
        Files.CreatedAt,
        Files.Name,
        Files.SizeInBytes,
        Files.Url,
        Files.ProcessStatusId,
        Files.ContentType
    FROM
        Files
    WHERE
        Files.IsDeleted = 0 AND
        Files.Id = @Id
END;