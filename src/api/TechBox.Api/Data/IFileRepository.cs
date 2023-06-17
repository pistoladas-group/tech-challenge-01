using TechBox.Api.Data.Dto;

namespace TechBox.Api.Data
{
    public interface IFileRepository
    {
        IEnumerable<ListFilesDto> ListFiles(int? pageNumber = null, int? pageSize = null);
    }
}