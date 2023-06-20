namespace TechBox.Api.Services;

public interface IFileStorageService
{
    Task UploadFile(IFormFile file);
    Task DeleteFile(string fileName);
    IEnumerable<string> SupportedFileExtensions();
}