CREATE OR ALTER PROCEDURE SP_ADD_FileLog
(
    @Id UNIQUEIDENTIFIER,
    @IsDeleted BIT,
    @CreatedAt DATETIME,
    @FileId UNIQUEIDENTIFIER,
    @ProcessStatusId TINYINT,
    @ProcessTypeId TINYINT,
    @ErrorMessage VARCHAR(1000),
    @StartedAt DATETIME,
    @FinishedAt DATETIME
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
        ProcessTypeId,
        ErrorMessage,
        StartedAt,
        FinishedAt
    )
    VALUES 
    (
        @Id, 
        @IsDeleted, 
        @CreatedAt, 
        @FileId,
        @ProcessStatusId,
        @ProcessTypeId,
        @ErrorMessage,
        @StartedAt,
        @FinishedAt
    );

    SELECT @@ROWCOUNT 'AffectedRows';
END;