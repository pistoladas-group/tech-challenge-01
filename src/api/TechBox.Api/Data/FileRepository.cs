using TechBox.Api.Data.Dto;

namespace TechBox.Api.Data
{
    public class FileRepository : IFileRepository
    {
        private readonly IStoredProcedureHandler _storedProcedureHandler;

        public FileRepository(IStoredProcedureHandler storedProcedureHandler)
        {
            _storedProcedureHandler = storedProcedureHandler;
        }

        public IEnumerable<ListFilesDto> ListFiles(int pageNumber, int pageSize)
        {
            return _storedProcedureHandler.ExecuteList<ListFilesDto>("SP_LST_Files", new ListProcedureDto(pageNumber, pageSize));
        }
    }
}
