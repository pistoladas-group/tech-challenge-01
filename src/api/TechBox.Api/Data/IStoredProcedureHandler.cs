using TechBox.Api.Data.Dto;

namespace TechBox.Api.Data;

public interface IStoredProcedureHandler
{
    Task<IEnumerable<T1>> ExecuteListAsync<T1>(string procedureName, ListProcedureParameters procedureParameter, int? commandTimeout = null);
    Task<T1> ExecuteGetAsync<T1>(string procedureName, GetProcedureParameters procedureParameter, int? commandTimeout = null);
    Task<int> ExecuteAddAsync(string procedureName, AddProcedureParameters procedureParameter, int? commandTimeout = null);
}
