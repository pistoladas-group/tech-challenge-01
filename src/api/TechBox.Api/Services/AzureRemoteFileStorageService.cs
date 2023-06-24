using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using TechBox.Api.Configurations;

namespace TechBox.Api.Services;

public class AzureRemoteFileStorageService : IRemoteFileStorageService
{
    private Uri _serviceUri { get; init; }
    private string _serviceContainerName { get; init; }
    private const string _supportedFileExtensions = "tif,tiff,bmp,jpg,jpeg,gif,png,eps,raw,cr2,nef,orf,sr2";
    private const byte _fileNameAndExtensionSplitLength = 2;

    public AzureRemoteFileStorageService()
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

    public ServiceResult ValidateFile(IFormFile file)
    {
        var result = new ServiceResult();

        var fileNameSplit = file.FileName.Split('.');

        if (fileNameSplit.Length != _fileNameAndExtensionSplitLength)
        {
            result.AddError(ApiErrors.InvalidFileName);
        }

        var extension = fileNameSplit[1].ToLower();
        var isSupportedExtension = SupportedFileExtensions().Contains(extension);

        if (!isSupportedExtension)
        {
            result.AddError(ApiErrors.UnsupportedExtension);
        }

        if (file.Length > 1048576 * 10) // 10 MB
        {
            result.AddError(ApiErrors.UnsupportedFileSize);
        }

        return result;
    }

    public async Task<Uri> UploadFileAsync(byte[] file, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_serviceUri, new DefaultAzureCredential());

        var containerClient = blobServiceClient.GetBlobContainerClient(_serviceContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        //TODO: tentar passar o content type

        var stream = new MemoryStream(file);

        try
        {
            await blobClient.UploadAsync(stream);
            return blobClient.Uri;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_serviceUri, new DefaultAzureCredential());

        var containerClient = blobServiceClient.GetBlobContainerClient(_serviceContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
    }

    private static IEnumerable<string> SupportedFileExtensions()
    {
        return _supportedFileExtensions.Split(',');
    }
}