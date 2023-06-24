using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using TechBox.Api.Common;
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

    public ExecutionResult ValidateFile(IFormFile file)
    {
        var result = new ExecutionResult();

        var fileNameSplit = file.FileName.Split('.');

        if (fileNameSplit.Length != _fileNameAndExtensionSplitLength)
        {
            result.AddError(ExecutionErrors.InvalidFileName);
        }

        var extension = fileNameSplit[1].ToLower();
        var isSupportedExtension = SupportedFileExtensions().Contains(extension);

        if (!isSupportedExtension)
        {
            result.AddError(ExecutionErrors.UnsupportedExtension);
        }

        if (file.Length > 1048576 * 10) // 10 MB
        {
            result.AddError(ExecutionErrors.UnsupportedFileSize);
        }

        return result;
    }

    public async Task<Uri> UploadFileAsync(byte[] file, string fileName, string contentType)
    {
        var blobServiceClient = new BlobServiceClient(_serviceUri, new DefaultAzureCredential());

        var containerClient = blobServiceClient.GetBlobContainerClient(_serviceContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        var stream = new MemoryStream(file);

        try
        {
            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
            });

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