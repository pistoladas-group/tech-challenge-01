namespace TechBox.Api.Services;

public class FileBackgroundService : IHostedService
{
    private readonly ILogger _logger;
    private IRemoteFileStorageService _remoteFileStorageService;   
    private ILocalFileStorageService _localFileStorageService;   

    public FileBackgroundService(ILogger<FileBackgroundService> logger, IRemoteFileStorageService remoteFileStorageService, ILocalFileStorageService localFileStorageService)
    {
        _logger = logger;
        _remoteFileStorageService = remoteFileStorageService;
        _localFileStorageService = localFileStorageService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        new Timer(ExecuteProcess, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    private void ExecuteProcess(object state)
    {
        //PEGAR OS ARQUIVOS PENDENTES, COMEÇANDO PELO TIPO DELETE
        
        //PROCESSA
        
        //PEGAR OS ARQUIVOS PENDENTES, PARA UPLOAD
        
        //PROCESSA
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
    
    private async Task UploadFile(Guid fileId, string fileName)
    {
        //fazer upload para o azure
        var file = _localFileStorageService.GetFileById(fileId, fileName);
        var uploadedFileUri = await _remoteFileStorageService.UploadFileAsync(file, fileName);

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