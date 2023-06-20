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
        var affectedRows = await _storedProcedureHandler.ExecuteAddAsync("SP_ADD_File", fileDto);

        if (affectedRows <= 0)
        {
            // TODO: Tratar erro
        }

        return fileDto.Id;
    }

    public async Task<int> DeleteFileByIdAsync(Guid fileId)
    {
        var affectedRows = await _storedProcedureHandler.ExecuteDeleteAsync("SP_DEL_FileById", new DeleteProcedureParameters(fileId));

        if (affectedRows <= 0)
        {
            // TODO: Tratar erro
        }

        return affectedRows;
    }

    public async Task<Guid> AddFileLogAsync(AddFileLogDto fileLogDto)
    {
        var affectedRows = await _storedProcedureHandler.ExecuteAddAsync("SP_ADD_FileLog", fileLogDto);

        if (affectedRows <= 0)
        {
            // TODO: Tratar erro
        }

        var affectedRowsUpdate = await UpdateFileProcessStatusByIdAsync(fileLogDto.FileId, fileLogDto.ProcessStatusId);

        if (affectedRowsUpdate <= 0)
        {
            // TODO: Rollback no FileLog acima
            // TODO: Tratar erro
        }

        return fileLogDto.Id;
    }

    public async Task<int> UpdateFileLogToProcessingByIdAsync(Guid fileLogId)
    {
        var fileLog = await _storedProcedureHandler.ExecuteGetAsync<FileLogDto>("SP_GET_FileLogById", new GetProcedureParameters(fileLogId));

        var processStatus = ProcessStatusEnum.Processing;

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync("SP_UPD_FileLogToProcessingById", new UpdateFileLogToProcessingByIdDto(fileLogId)
        {
            ProcessStatusId = processStatus,
            StartedAt = DateTime.UtcNow
        });

        if (affectedRows <= 0)
        {
            // TODO: Tratar erro
        }

        // TODO: Achei interessante atualizar a tabela Files aqui pq deixa a
        // responsabilidade para o Repository (que é especialista em interfacear o banco) de saber quais tabelas
        // desnormalizadas deve-se atualizar, dessa forma evita triggers e updates "escondidos" nas procs

        var affectedRowsUpdate = await UpdateFileProcessStatusByIdAsync(fileLog.FileId, processStatus);

        if (affectedRowsUpdate <= 0)
        {
            // TODO: Rollback no FileLog acima
            // TODO: Tratar erro
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileLogToSuccessByIdAsync(Guid fileLogId)
    {
        var fileLog = await _storedProcedureHandler.ExecuteGetAsync<FileLogDto>("SP_GET_FileLogById", new GetProcedureParameters(fileLogId));

        var processStatus = ProcessStatusEnum.Success;

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync("SP_UPD_FileLogToSuccessById", new UpdateFileLogToSuccessByIdDto(fileLogId)
        {
            ProcessStatusId = processStatus,
            FinishedAt = DateTime.UtcNow
        });

        if (affectedRows <= 0)
        {
            // TODO: Tratar erro
        }

        // TODO: Achei interessante atualizar a tabela Files aqui pq deixa a
        // responsabilidade para o Repository (que é especialista em interfacear o banco) de saber quais tabelas
        // desnormalizadas deve-se atualizar, dessa forma evita triggers e updates "escondidos" nas procs

        var affectedRowsUpdate = await UpdateFileProcessStatusByIdAsync(fileLog.FileId, processStatus);

        if (affectedRowsUpdate <= 0)
        {
            // TODO: Rollback no FileLog acima
            // TODO: Tratar erro
        }

        return affectedRows;
    }

    public async Task<int> UpdateFileLogToFailedByIdAsync(Guid fileLogId, string errorMessage)
    {
        var fileLog = await _storedProcedureHandler.ExecuteGetAsync<FileLogDto>("SP_GET_FileLogById", new GetProcedureParameters(fileLogId));

        var processStatus = ProcessStatusEnum.Failed;

        var affectedRows = await _storedProcedureHandler.ExecuteUpdateAsync("SP_UPD_FileLogToFailedById", new UpdateFileLogToFailedByIdDto(fileLogId)
        {
            ProcessStatusId = processStatus,
            FinishedAt = DateTime.UtcNow,
            ErrorMessage = errorMessage
        });

        if (affectedRows <= 0)
        {
            // TODO: Tratar erro
        }

        // TODO: Achei interessante atualizar a tabela Files aqui pq deixa a
        // responsabilidade para o Repository (que é especialista em interfacear o banco) de saber quais tabelas
        // desnormalizadas deve-se atualizar, dessa forma evita triggers e updates "escondidos" nas procs

        var affectedRowsUpdate = await UpdateFileProcessStatusByIdAsync(fileLog.FileId, processStatus);

        if (affectedRowsUpdate <= 0)
        {
            // TODO: Rollback no FileLog acima
            // TODO: Tratar erro
        }

        return affectedRows;
    }


    private async Task<int> UpdateFileProcessStatusByIdAsync(Guid fileId, ProcessStatusEnum processStatusId)
    {
        return await _storedProcedureHandler.ExecuteUpdateAsync("SP_UPD_FileProcessStatusById", new UpdateFileProcessStatusByIdDto(fileId)
        {
            ProcessStatusId = processStatusId
        });
    }
}
