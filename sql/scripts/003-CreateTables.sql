IF OBJECT_ID(N'Files') IS NULL
BEGIN
    CREATE TABLE [Files] (
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [IsDeleted] BIT NOT NULL,
        [CreatedAt] DATETIME NOT NULL,

        [Name] VARCHAR(100) NOT NULL,
        [SizeInBytes] INT NOT NULL,
        [Url] VARCHAR(2500) NULL,
        [ContentType] VARCHAR(100) NOT NULL,
        [ProcessStatusId] TINYINT NOT NULL,
        CONSTRAINT [PK_Files] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Files_ProcessStatus] FOREIGN KEY (ProcessStatusId) REFERENCES ProcessStatus (Id)
    );
END;
GO

IF OBJECT_ID(N'FileLogs') IS NULL
BEGIN
    CREATE TABLE [FileLogs] (
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [IsDeleted] BIT NOT NULL,
        [CreatedAt] DATETIME NOT NULL,

        [FileId] UNIQUEIDENTIFIER NOT NULL,
        [ProcessTypeId] TINYINT NOT NULL,
        [ProcessStatusId] TINYINT NOT NULL,
        [ErrorMessage] VARCHAR(1000) NULL,
        [StartedAt] DATETIME NULL,
        [FinishedAt] DATETIME NULL,
        CONSTRAINT [PK_FileLogs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FileLogs_ProcessStatus] FOREIGN KEY (ProcessStatusId) REFERENCES ProcessStatus (Id),
        CONSTRAINT [FK_FileLogs_ProcessTypes] FOREIGN KEY (ProcessTypeId) REFERENCES ProcessTypes (Id)
    );
END;
GO