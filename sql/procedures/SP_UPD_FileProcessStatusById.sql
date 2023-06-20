CREATE OR ALTER PROCEDURE SP_UPD_FileProcessStatusById
(
    @Id UNIQUEIDENTIFIER,
    @ProcessStatusId TINYINT
)
AS
BEGIN
    UPDATE 
        Files
    SET
        ProcessStatusId = @ProcessStatusId
    WHERE
        Files.Id = @Id;

    SELECT @@ROWCOUNT 'AffectedRows';
END;