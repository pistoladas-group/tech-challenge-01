using Serilog;
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

    public async Task<IEnumerable<Guid>> ListPendingFileIdsAsync(int pageNumber, int pageSize)
    {
        return await _storedProcedureHandler.ExecuteListAsync<Guid>("SP_LST_PendingFiles", new ListProcedureParameters(pageNumber, pageSize));
    }

    public async Task<IEnumerable<ListFilePendingLogsResponseDto>> ListFilePendingLogsAsync(Guid fileId, int pageNumber, int pageSize)
    {
        return await _storedProcedureHandler.ExecuteListAsync<ListFilePendingLogsResponseDto>("SP_LST_FilePendingLogs", new ListFilePendingLogsProcedureParameters(fileId, pageNumber, pageSize));
    }

    public async Task<FileDto?> GetFileByIdAsync(Guid fileId)
    {
        try
        {
            return await _storedProcedureHandler.ExecuteGetAsync<FileDto>("SP_GET_FileById", new GetProcedureParameters(fileId));
        }
        catch (InvalidOperationException e) when (e.Message == "Sequence contains no elements")
        {
            Log.Error(e, "Error executing method GetFileByIdAsync");
            return null;
        }
    }

    public async Task<bool> CheckFileLogByFileIdAndProcessTypeIdAsync(Guid fileId, ProcessTypesEnum processTypesId)
    {
            return await _storedProcedureHandler.ExecuteCheckAsync("SP_CHK_FileLogByFileIdAndProcessType", new GetFileLogByFileIdAndProcessTypeProcedureParameters(fileId, processTypesId));
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

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileProcessStatusByIdProcedureParameters(fileId)
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

    public async Task<int> UpdateFileLogToProcessingByFileAndProcessTypeIdAsync(Guid fileId, ProcessTypesEnum processTypeId)
    {
        var procedureName = "SP_UPD_FileLogToProcessingByFileAndProcessTypeId";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileLogToProcessingByFileAndProcessTypeProcedureParameters(fileId, processTypeId));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileLogToSuccessByFileAndProcessTypeIdAsync(Guid fileId, ProcessTypesEnum processTypeId)
    {
        var procedureName = "SP_UPD_FileLogToSuccessByFileAndProcessTypeId";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileLogToSuccessByFileAndProcessTypeProcedureParameters(fileId, processTypeId));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileLogToFailedByFileAndProcessTypeId(Guid fileId, ProcessTypesEnum processTypeId, string errorMessage)
    {
        var procedureName = "SP_UPD_FileLogToFailedByFileAndProcessTypeId";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileLogToFailedByFileAndProcessTypeIdProcedureParameters(fileId, processTypeId, errorMessage));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileByIdAsync(Guid fileId, Uri? fileUrl, bool isDeleted)
    {
        var procedureName = "SP_UPD_FileById";

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync(procedureName, new UpdateFileByIdProcedureParameters(fileId, fileUrl, isDeleted));

        if (affectedRows <= 0)
        {
            throw new ProcedureExecutionException(procedureName);
        }

        return affectedRows;
    }

    public async Task<bool> CheckIfFileExistsByFileNameAsync(string fileName)
    {
        return await _storedProcedureHandler.ExecuteCheckAsync("SP_CHK_FileByFileName", new { fileName });
    }
    
    public async Task<IEnumerable<FileDto>> ListFilesByFileName(string fileName)
    {
        return await _storedProcedureHandler.ExecuteListAsync<FileDto>("SP_LST_FilesByFileName", new { fileName });
    }
}
