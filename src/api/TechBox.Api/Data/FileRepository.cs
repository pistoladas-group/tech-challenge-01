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

        var affectedRows = await _storedProcedureHandler.ExecuteAddAsync("SP_ADD_File", fileDto);

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

    public async Task<int> UpdateFileLogToProcessingByFileIdAsync(Guid fileId, ProcessTypesEnum processTypesId)
    {
        var procedureName = "SP_UPD_FileLogToProcessingByFileIdAndProcessType";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileLogToProcessingByFileIdAndProcessTypeDto(fileId, processTypesId));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileLogToSuccessByFileIdAsync(Guid fileId, ProcessTypesEnum processTypesId)
    {
        var procedureName = "SP_UPD_FileLogToSuccessByFileIdAndProcessType";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileLogToSuccessByFileIdAndProcessTypeDto(fileId, processTypesId));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileLogToFailedByIdAsync(Guid fileId, ProcessTypesEnum processTypesId,  string errorMessage)
    {
        var procedureName = "SP_UPD_FileLogToFailedByFileIdAndProcessType";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileLogToFailedByFileIdAndProcessTypeDto(fileId, processTypesId, errorMessage));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }
}
