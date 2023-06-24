using TechBox.Api.Data;

namespace TechBox.Api.Services;

public class FileBackgroundService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IRemoteFileStorageService _remoteFileStorageService;
    private readonly ILocalFileStorageService _localFileStorageService;

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
        new Timer(ExecuteProcessAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    private async void ExecuteProcessAsync(object state)
    {
        //PEGAR OS ARQUIVOS PENDENTES, COMEÇANDO PELO TIPO DELETE

        //PROCESSA

        //PEGAR OS ARQUIVOS PENDENTES, PARA UPLOAD

        //PROCESSA

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scopedService = scope.ServiceProvider.GetRequiredService<IFileRepository>();

            // var fileLogs = await scopedService.ListFileLogs....
        }
    }

    private void DeleteFile()
    {
        //DELETA ARQUIVO DO AZURE

        //se erro,
        //  atualiza status para erro e grava mensagem

        //se sucesso,

        //  atualiza o status para done

        //DELETA ARQUIVO LOCAL

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
        return Task.CompletedTask;
    }
}