using TechBox.Api.Data.Dto;

namespace TechBox.Api.Data
{
    public class FileRepository : IFileRepository
    {
        private readonly IStoredProcedureHandler _storedProcedure;

        public FileRepository(IStoredProcedureHandler storedProcedure)
        {
            _storedProcedure = storedProcedure;
        }

        public IEnumerable<ListFilesDto> ListFiles(int? pageNumber = null, int? pageSize = null)
        {
            return _storedProcedure.ExecuteList<ListFilesDto>("SP_LST_Files", new ListProcedureDto() { PageNumber = pageNumber, PageSize = pageSize });
        }
    }
}
