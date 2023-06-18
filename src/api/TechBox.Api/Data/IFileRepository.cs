using TechBox.Api.Data.Dto;

namespace TechBox.Api.Data;

public interface IFileRepository
{
    Task<IEnumerable<FileDto>> ListFilesAsync(int pageNumber, int pageSize);
    Task<FileDto> GetFileByIdAsync(Guid fileId);
    Task<Guid> AddFileAsync(AddFileDto fileDto);
}
