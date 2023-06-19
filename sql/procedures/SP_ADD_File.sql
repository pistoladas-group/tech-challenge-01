CREATE OR ALTER PROCEDURE SP_ADD_File
(
    @Id UNIQUEIDENTIFIER,
    @IsDeleted BIT,
    @CreatedAt DATETIME,
    @Name VARCHAR(100),
    @SizeInBytes INT,
    @Url VARCHAR(2500),
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
        Url, 
        ProcessStatusId
    )
    VALUES 
    (
        @Id, 
        @IsDeleted, 
        @CreatedAt,
        @Name,
        @SizeInBytes,
        @Url,
        @ProcessStatusId
    );

    SELECT @@ROWCOUNT 'AffectedRows';
END;