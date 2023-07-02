CREATE OR ALTER PROCEDURE SP_CHK_FileByFileName
(
	@FileName VARCHAR(100)
)
AS
BEGIN
    DECLARE @Exists BIT = 0;
    
    SELECT
        @Exists = 1
    FROM
        Files
    WHERE
        Files.Name = @FileName AND
        Files.IsDeleted = 0;
    
    SELECT @Exists 'Exists';
END;