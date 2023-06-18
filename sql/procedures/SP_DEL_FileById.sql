CREATE OR ALTER PROCEDURE SP_DEL_FileById
(
	@Id UNIQUEIDENTIFIER
)
AS
BEGIN
    UPDATE Files
    SET
        Files.IsDeleted = 1
    WHERE
        Files.IsDeleted = 0 AND
        Files.Id = @Id
    
    SELECT @@ROWCOUNT 'AffectedRows';
END;