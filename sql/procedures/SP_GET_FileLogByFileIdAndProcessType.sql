CREATE OR ALTER PROCEDURE SP_CHK_FileLogByFileIdAndProcessType
(
	@Id UNIQUEIDENTIFIER,
	@ProcessTypeId TINYINT
)
AS
BEGIN
    DECLARE @Exists BIT = 0;
    
    SELECT
        @Exists = 1
    FROM
        FileLogs
    WHERE
        FileLogs.IsDeleted = 0 AND
        FileLogs.FileId = @Id AND
        FileLogs.ProcessTypeId = @ProcessTypeId;
    
    SELECT @Exists 'Exists';
END;