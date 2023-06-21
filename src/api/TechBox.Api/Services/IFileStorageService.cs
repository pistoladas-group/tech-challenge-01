namespace TechBox.Api.Services;

public interface IFileStorageService
{
    bool ValidateFile(IFormFile file);
    Task UploadFileAsync(IFormFile file);
    Task DeleteFileAsync(string fileName);
}