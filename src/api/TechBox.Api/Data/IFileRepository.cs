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
    Task<int> UpdateFileLogToProcessingByFileIdAsync(Guid fileId);
    Task<int> UpdateFileLogToSuccessByFileIdAndProcessTypeAsync(Guid fileId, ProcessTypesEnum processTypeId);
    Task<int> UpdateFileLogToFailedByIdAsync(Guid fileId, string errorMessage);
    Task<IEnumerable<Guid>> ListPendingFileIdsAsync(int pageNumber, int pageSize);
    Task<FileLogDto?> GetFileLogByFileIdAndProcessTypeIdAsync(Guid fileId, ProcessTypesEnum processTypesId);
    Task<IEnumerable<ListFilePendingLogsResponseDto>> ListFilePendingLogsAsync(Guid fileId, int pageNumber, int pageSize);
    Task<int> UpdateFileByIdAsync(Guid fileId, Uri? fileUrl, bool isDeleted);
}
