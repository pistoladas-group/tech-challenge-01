CREATE OR ALTER PROCEDURE SP_ADD_FileLog
(
    @Id UNIQUEIDENTIFIER,
    @IsDeleted BIT,
    @CreatedAt DATETIME,
    @FileId UNIQUEIDENTIFIER,
    @ProcessStatusId TINYINT,
    @ProcessTypeId TINYINT,
    @ErrorMessage VARCHAR(1000) = NULL,
    @StartedAt DATETIME,
    @FinishedAt DATETIME = NULL
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