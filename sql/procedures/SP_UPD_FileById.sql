CREATE OR ALTER PROCEDURE SP_UPD_FileById
(
    @FileId UNIQUEIDENTIFIER,
    @Url VARCHAR(2500) = NULL,
    @IsDeleted BIT = NULL
)
AS
BEGIN
    UPDATE 
        Files
    SET
        Files.ProcessStatusId = 4,
        Files.Url = @Url,
        Files.IsDeleted = @IsDeleted
    WHERE
        Files.Id = @FileId;

    SELECT @@ROWCOUNT 'AffectedRows';
END;