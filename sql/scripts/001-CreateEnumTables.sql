USE TechBox
GO

IF OBJECT_ID(N'ProcessStatus') IS NULL
BEGIN
    CREATE TABLE [ProcessStatus] (
        [Id] TINYINT NOT NULL,
        [Name] VARCHAR(50) NOT NULL,
        CONSTRAINT [PK_ProcessStatus] PRIMARY KEY ([Id])
    );
END;
GO

IF OBJECT_ID(N'ProcessTypes') IS NULL
BEGIN
    CREATE TABLE [ProcessTypes] (
        [Id] TINYINT NOT NULL,
        [Name] VARCHAR(50) NOT NULL,
        CONSTRAINT [PK_ProcessTypes] PRIMARY KEY ([Id])
    );
END;
GO