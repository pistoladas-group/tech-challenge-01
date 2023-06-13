USE TechBox
GO

BEGIN
	-- ProcessStatus
    DECLARE @Id TINYINT = 1;
    DECLARE @Name VARCHAR(50) = 'Pending';
    IF NOT EXISTS (SELECT 1 FROM ProcessStatus WHERE ProcessStatus.Id = @Id AND ProcessStatus.Name = @Name)
    BEGIN
        INSERT INTO ProcessStatus(Id, Name) VALUES(@Id, @Name)
    END;

	SET @Id = 2;
	SET @Name = 'Processing';
    IF NOT EXISTS (SELECT 1 FROM ProcessStatus WHERE ProcessStatus.Id = @Id AND ProcessStatus.Name = @Name)
    BEGIN
        INSERT INTO ProcessStatus(Id, Name) VALUES(@Id, @Name)
    END;

	SET @Id = 3;
	SET @Name = 'Failed';
    IF NOT EXISTS (SELECT 1 FROM ProcessStatus WHERE ProcessStatus.Id = @Id AND ProcessStatus.Name = @Name)
    BEGIN
        INSERT INTO ProcessStatus(Id, Name) VALUES(@Id, @Name)
    END;

	SET @Id = 4;
	SET @Name = 'Success';
    IF NOT EXISTS (SELECT 1 FROM ProcessStatus WHERE ProcessStatus.Id = @Id AND ProcessStatus.Name = @Name)
    BEGIN
        INSERT INTO ProcessStatus(Id, Name) VALUES(@Id, @Name)
    END;

    -- ProcessTypes
    SET @Id = 1;
	SET @Name = 'Upload';
    IF NOT EXISTS (SELECT 1 FROM ProcessTypes WHERE ProcessTypes.Id = @Id AND ProcessTypes.Name = @Name)
    BEGIN
        INSERT INTO ProcessTypes(Id, Name) VALUES(@Id, @Name)
    END;

	SET @Id = 2;
	SET @Name = 'Delete';
    IF NOT EXISTS (SELECT 1 FROM ProcessTypes WHERE ProcessTypes.Id = @Id AND ProcessTypes.Name = @Name)
    BEGIN
        INSERT INTO ProcessTypes(Id, Name) VALUES(@Id, @Name)
    END;

END;
GO