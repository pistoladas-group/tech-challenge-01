using System.Net;

namespace TechBox.Api.Services;

public class LocalFileStorageService : ILocalFileStorageService
{
    private const string _tempDirectoryName = "tmp";
    
    public LocalFileStorageService()
    {
        if (!Directory.Exists(_tempDirectoryName))
        {
            Directory.CreateDirectory(_tempDirectoryName);
        }    
    }
    
    public bool SaveFile(IFormFile file, Guid fileId)
    {
        var fileStream = new MemoryStream();
        file.CopyTo(fileStream);

        if (!Directory.Exists($"{_tempDirectoryName}/{fileId}"))
        {
            Directory.CreateDirectory($"{_tempDirectoryName}/{fileId}");
        }
        
        File.WriteAllBytes($"{_tempDirectoryName}/{fileId}/{file.FileName}", fileStream.ToArray());

        return true;
    }

    public bool DeleteFile(Guid fileId, string fileName)
    {
        if (!File.Exists($"{_tempDirectoryName}/{fileId}/{fileName}"))
        {
            //TODO: LOG 
            return true;
        }

        try
        {
            File.Delete($"{_tempDirectoryName}/{fileId}/{fileName}");
            Directory.Delete($"{_tempDirectoryName}/{fileId}");
        }
        catch (Exception e)
        {
            //TODO: LOG
        }
        
        return true;
    }
    
    public byte[] GetFileById(Guid fileId, string fileName)
    {
        if (!File.Exists($"{_tempDirectoryName}/{fileId}/{fileName}"))
        {
            //LOG
        }

        return File.ReadAllBytes($"{_tempDirectoryName}/{fileId}/{fileName}");
    }
}