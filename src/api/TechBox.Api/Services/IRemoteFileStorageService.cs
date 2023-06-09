using TechBox.Api.Common;

namespace TechBox.Api.Services;

public interface IRemoteFileStorageService
{
    ExecutionResult ValidateFile(IFormFile file);
    Task<Uri> UploadFileAsync(byte[] file, string fileName, string contentType);
    Task DeleteFileAsync(string fileName);
}