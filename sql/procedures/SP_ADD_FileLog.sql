CREATE OR ALTER PROCEDURE SP_ADD_FileLog
(
    @Id UNIQUEIDENTIFIER,
    @IsDeleted BIT,
    @CreatedAt DATETIME,
    @FileId UNIQUEIDENTIFIER,
    @ProcessTypeId TINYINT,
    @ProcessStatusId TINYINT
)
AS
BEGIN
    INSERT INTO FileLogs
    (
        Id, 
        IsDeleted, 
        CreatedAt, 
        FileId,
        ProcessStatusId,
        ProcessTypeId
    )
    VALUES 
    (
        @Id, 
        @IsDeleted, 
        @CreatedAt, 
        @FileId,
        @ProcessStatusId,
        @ProcessTypeId
    );

    SELECT @@ROWCOUNT 'AffectedRows';
END;