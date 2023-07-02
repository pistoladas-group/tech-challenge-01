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
    Task<int> UpdateFileLogToProcessingByFileAndProcessTypeIdAsync(Guid fileId, ProcessTypesEnum processTypeId);
    Task<int> UpdateFileLogToSuccessByFileAndProcessTypeIdAsync(Guid fileId, ProcessTypesEnum processTypeId);
    Task<int> UpdateFileLogToFailedByFileAndProcessTypeId(Guid fileId, ProcessTypesEnum processTypeId, string errorMessage);
    Task<IEnumerable<Guid>> ListPendingFileIdsAsync(int pageNumber, int pageSize);
    Task<bool> CheckFileLogByFileIdAndProcessTypeIdAsync(Guid fileId, ProcessTypesEnum processTypesId);
    Task<IEnumerable<ListFilePendingLogsResponseDto>> ListFilePendingLogsAsync(Guid fileId, int pageNumber, int pageSize);
    Task<int> UpdateFileByIdAsync(Guid fileId, Uri? fileUrl, bool isDeleted);
    Task<bool> CheckIfFileExistsByFileNameAsync(string fileName);
    Task<IEnumerable<FileDto>> ListFilesByFileName(string fileName);
}
