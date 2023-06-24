using TechBox.Api.Data;

namespace TechBox.Api.Services;

public class FileBackgroundService : IHostedService, IDisposable
{
    private readonly ILogger _logger;
    private IRemoteFileStorageService _remoteFileStorageService;   
    private ILocalFileStorageService _localFileStorageService;   
    private IServiceScopeFactory _serviceScopeFactory;   
    private Timer? _timer;   

    public FileBackgroundService(
        ILogger<FileBackgroundService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IRemoteFileStorageService remoteFileStorageService,
        ILocalFileStorageService localFileStorageService)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _remoteFileStorageService = remoteFileStorageService;
        _localFileStorageService = localFileStorageService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(ExecuteProcessAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    private async void ExecuteProcessAsync(object state)
    {
        //PEGAR OS ARQUIVOS PENDENTES, COMEÇANDO PELO TIPO DELETE

        //PROCESSA

        //PEGAR OS ARQUIVOS PENDENTES, PARA UPLOAD

        //PROCESSA

        using var scope = _serviceScopeFactory.CreateScope();
        var scopedService = scope.ServiceProvider.GetRequiredService<IFileRepository>();

        // var fileLogs = await scopedService.ListFileLogs....
    }

    private void DeleteFile()
    {
        //DELETA ARQUIVO DO AZURE
        
        //se erro,
        //  atualiza status para erro e grava mensagem

        //se sucesso,

        //  atualiza o status para done

        //DELETA ARQUIVO LOCAL
        //
        // // TODO: #### Begin Background ####
        //
        // await _fileRepository.UpdateFileLogToProcessingByIdAsync(fileLogId);
        //
        // await _remoteFileStorageService.DeleteFileAsync(file.Name);
        //
        // // TODO: Se sucesso:
        // //              - Atualizar Files (Url = NULL, IsDeleted = 1, ProcessStatusId = Success)
        // //              - Atualizar FileLogs (ProcessStatusId = Success, FinishedAt = DateTime.UtcNow)
        // await _fileRepository.UpdateFileLogToSuccessByIdAsync(fileLogId);
        //
        // await _fileRepository.DeleteFileByIdAsync(fileId);
        //
        // // TODO: Se erro:
        // //              - Atualizar Files (ProcessStatusId = Failed)
        // //              - Atualizar FileLogs (ProcessStatusId = Failed, FinishedAt = DateTime.UtcNow, ErrorMessage = "<error>")
        // // await _fileRepository.UpdateFileLogToFailedByIdAsync(fileLogId, "An error ocurred");
        //
        // // TODO: #### End Background ####

    }

    private async Task UploadFileAsync(Guid fileId, string fileName, string contentType)
    {
        //fazer upload para o azure
        var file = _localFileStorageService.GetFileById(fileId, fileName);

        var uploadedFileUri = await _remoteFileStorageService.UploadFileAsync(file, fileName, contentType);

        //se erro,
        //  atualiza status para erro e grava mensagem

        //se sucesso,
        //  gravar url no banco

        //  atualiza o status para done

        //  deletar o arquivo local
        _localFileStorageService.DeleteFile(fileId, fileName);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);
        
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}