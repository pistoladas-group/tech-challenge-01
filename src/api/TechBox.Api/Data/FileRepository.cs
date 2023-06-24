using TechBox.Api.Common;
using TechBox.Api.Data.Dto;
using TechBox.Api.Models;

namespace TechBox.Api.Data;

public class FileRepository : IFileRepository
{
    private readonly IStoredProcedureHandler _storedProcedureHandler;

    public FileRepository(IStoredProcedureHandler storedProcedureHandler)
    {
        _storedProcedureHandler = storedProcedureHandler;
    }

    public async Task<IEnumerable<FileDto>> ListFilesAsync(int pageNumber, int pageSize)
    {
        return await _storedProcedureHandler.ExecuteListAsync<FileDto>("SP_LST_Files", new ListProcedureParameters(pageNumber, pageSize));
    }

    public async Task<FileDto?> GetFileByIdAsync(Guid fileId)
    {
        try
        {
            return await _storedProcedureHandler.ExecuteGetAsync<FileDto>("SP_GET_FileById", new GetProcedureParameters(fileId));
        }
        catch (InvalidOperationException e) when (e.Message == "Sequence contains no elements")
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Guid> AddFileAsync(AddFileDto fileDto)
    {
        var procedureName = "SP_ADD_File";

        var affectedRows = await _storedProcedureHandler.ExecuteAddAsync(procedureName, fileDto);

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return fileDto.Id;
    }

    public async Task<int> DeleteFileByIdAsync(Guid fileId)
    {
        var procedureName = "SP_DEL_FileById";

        var affectedRows = await _storedProcedureHandler.ExecuteDeleteAsync(procedureName, new DeleteProcedureParameters(fileId));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileProcessStatusByIdAsync(Guid fileId, ProcessStatusEnum processStatusId)
    {
        var procedureName = "SP_UPD_FileProcessStatusById";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileProcessStatusByIdDto(fileId)
        {
            ProcessStatusId = processStatusId
        });

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<Guid> AddFileLogAsync(AddFileLogDto fileLogDto)
    {
        var procedureName = "SP_ADD_FileLog";

        var affectedRows = await _storedProcedureHandler.ExecuteAddAsync(procedureName, fileLogDto);

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return fileLogDto.Id;
    }

    public async Task<int> UpdateFileLogToProcessingByIdAsync(Guid fileLogId)
    {
        var procedureName = "SP_UPD_FileLogToProcessingById";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileLogToProcessingByIdDto(fileLogId));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileLogToSuccessByIdAsync(Guid fileLogId)
    {
        var procedureName = "SP_UPD_FileLogToSuccessById";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileLogToSuccessByIdDto(fileLogId));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileLogToFailedByIdAsync(Guid fileLogId, string errorMessage)
    {
        var procedureName = "SP_UPD_FileLogToFailedById";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileLogToFailedByIdDto(fileLogId, errorMessage));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }
}
