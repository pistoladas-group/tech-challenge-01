namespace TechBox.Api.Services;

public interface ILocalFileStorageService
{
    void SaveFile(IFormFile file, Guid fileId);
    void DeleteFile(Guid fileId, string fileName);
    byte[] GetFileById(Guid fileId, string fileName);
}