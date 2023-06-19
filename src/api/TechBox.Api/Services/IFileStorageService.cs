namespace TechBox.Api.Services;

public interface IFileStorageService
{
    Task UploadFile(IFormFile file);
    Task DeleteFile(IFormFile file);
    IEnumerable<string> SupportedFileExtensions();
}