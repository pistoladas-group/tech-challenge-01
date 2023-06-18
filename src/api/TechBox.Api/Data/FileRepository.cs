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

        public async Task<IEnumerable<FileDto>> ListFilesAsync(int pageNumber, int pageSize)
        {
            return await _storedProcedureHandler.ExecuteListAsync<FileDto>("SP_LST_Files", new ListProcedureParameters(pageNumber, pageSize));
        }

        public async Task<FileDto> GetFileByIdAsync(Guid fileId)
        {
            return await _storedProcedureHandler.ExecuteGetAsync<FileDto>("SP_GET_FileById", new GetProcedureParameters(fileId));
        }
    }
}
