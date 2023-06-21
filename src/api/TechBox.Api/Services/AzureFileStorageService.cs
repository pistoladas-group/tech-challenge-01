using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using TechBox.Api.Configurations;

namespace TechBox.Api.Services;

public class AzureFileStorageService : IFileStorageService
{
    private Uri _serviceUri { get; init; }
    private string _serviceContainerName { get; init; }
    private const string _supportedFileExtensions = "tif,tiff,bmp,jpg,jpeg,gif,png,eps,raw,cr2,nef,orf,sr2";
    private const byte _fileNameAndExtensionSplitLength = 2;

    public AzureFileStorageService()
    {
        var serviceUri = Environment.GetEnvironmentVariable(EnvironmentVariables.StorageAccountUrl);
        var serviceContainerName = Environment.GetEnvironmentVariable(EnvironmentVariables.StorageAccountContainerName);

        if (string.IsNullOrEmpty(serviceUri))
        {
            throw new ApplicationException("The StorageAccountUrl environment variable was not set");
        }

        if (string.IsNullOrEmpty(serviceContainerName))
        {
            throw new ApplicationException("The StorageAccountContainerName environment variable was not set");
        }

        _serviceUri = new Uri(serviceUri);
        _serviceContainerName = serviceContainerName;
    }

    public bool ValidateFile(IFormFile file) //TODO: criar classe de resposta com mensagens de erro
    {
        var fileNameSplit = file.FileName.Split('.');

        if (fileNameSplit.Length != _fileNameAndExtensionSplitLength)
        {
            return false;
        }

        var extension = fileNameSplit[1].ToLower();
        var isSupportedExtension = SupportedFileExtensions().Contains(extension);

        if (!isSupportedExtension)
        {
            return false;
        }
        
        // TODO: Limitar tamanho do arquivo

        return true;
    }

    public async Task UploadFileAsync(IFormFile file)
    {
        //TODO: mudar status para processing e criar serviço de processamento (serviço deve validar variáveis de ambiente)
        //TODO: gravar o tipo de processamento como Inclusão
        var blobServiceClient = new BlobServiceClient(_serviceUri, new DefaultAzureCredential());

        var containerClient = blobServiceClient.GetBlobContainerClient(_serviceContainerName);
        var blobClient = containerClient.GetBlobClient(file.FileName);

        //TODO: aqui pode dar erro.
        //TODO: tentar passar o content type
        await using (var stream = file.OpenReadStream())
        {
                await blobClient.UploadAsync(stream);
        }
    }

    public async Task DeleteFileAsync(string fileName)
    {
        // TODO: Daqui...

        var blobServiceClient = new BlobServiceClient(_serviceUri, new DefaultAzureCredential());

        var containerClient = blobServiceClient.GetBlobContainerClient(_serviceContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

        // TODO: ... até aqui pode dar erro
    }

    private static IEnumerable<string> SupportedFileExtensions()
    {
        return _supportedFileExtensions.Split(',');
    }
}