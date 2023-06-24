CREATE OR ALTER PROCEDURE SP_ADD_File
(
    @Id UNIQUEIDENTIFIER,
    @IsDeleted BIT,
    @CreatedAt DATETIME,
    @Name VARCHAR(100),
    @SizeInBytes INT,
    @ContentType VARCHAR(100),
    @ProcessStatusId TINYINT
)
AS
BEGIN
    INSERT INTO Files
    (
        Id, 
        IsDeleted, 
        CreatedAt, 
        Name, 
        SizeInBytes, 
        ContentType, 
        ProcessStatusId
    )
    VALUES 
    (
        @Id, 
        @IsDeleted, 
        @CreatedAt,
        @Name,
        @SizeInBytes,
        @ContentType,
        @ProcessStatusId
    );

    SELECT @@ROWCOUNT 'AffectedRows';
END;