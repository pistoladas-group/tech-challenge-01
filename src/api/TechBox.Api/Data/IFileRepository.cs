using TechBox.Api.Data.Dto;
using TechBox.Api.Models;

namespace TechBox.Api.Data;

public interface IFileRepository
{
    Task<IEnumerable<FileDto>> ListFilesAsync(int pageNumber, int pageSize);
    Task<FileDto?> GetFileByIdAsync(Guid fileId);
    Task<Guid> AddFileAsync(AddFileDto fileDto);
    Task<int> DeleteFileByIdAsync(Guid fileId);
    Task<int> UpdateFileProcessStatusByIdAsync(Guid fileId, ProcessStatusEnum processStatusId);
    Task<Guid> AddFileLogAsync(AddFileLogDto fileLogDto);
    Task<int> UpdateFileLogToProcessingByFileIdAsync(Guid fileId, ProcessTypesEnum processTypesId);
    Task<int> UpdateFileLogToSuccessByFileIdAsync(Guid fileId, ProcessTypesEnum processTypesId);
    Task<int> UpdateFileLogToFailedByIdAsync(Guid fileId, ProcessTypesEnum processTypesId, string errorMessage);
}
