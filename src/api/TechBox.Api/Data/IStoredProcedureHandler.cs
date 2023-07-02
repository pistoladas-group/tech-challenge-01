namespace TechBox.Api.Data;

public interface IStoredProcedureHandler
{
    Task<IEnumerable<T1>> ExecuteListAsync<T1>(string procedureName, object procedureParameter, int? commandTimeout = null);
    Task<T1> ExecuteGetAsync<T1>(string procedureName, object procedureParameter, int? commandTimeout = null);
    Task<int> ExecuteAddAsync(string procedureName, object procedureParameter, int? commandTimeout = null);
    Task<int> ExecuteUpdateAsync(string procedureName, object procedureParameter, int? commandTimeout = null);
    Task<int> ExecuteDeleteAsync(string procedureName, object procedureParameter, int? commandTimeout = null);
    Task<bool> ExecuteCheckAsync(string procedureName, object procedureParameter, int? commandTimeout = null);
}
