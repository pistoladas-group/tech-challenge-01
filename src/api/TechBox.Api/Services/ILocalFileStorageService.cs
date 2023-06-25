namespace TechBox.Api.Services;

public interface ILocalFileStorageService
{
    bool SaveFile(IFormFile file, Guid fileId);
    void DeleteFile(Guid fileId, string fileName);
    byte[] GetFileById(Guid fileId, string fileName);
}