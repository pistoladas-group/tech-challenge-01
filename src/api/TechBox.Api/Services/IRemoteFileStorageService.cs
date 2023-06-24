namespace TechBox.Api.Services;

public interface IRemoteFileStorageService
{
    ServiceResult ValidateFile(IFormFile file);
    Task<Uri> UploadFileAsync(byte[] file, string fileName);
    Task DeleteFileAsync(string fileName);
}