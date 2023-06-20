using System.Data;

using Dapper;

using TechBox.Api.Data.Dto;

namespace TechBox.Api.Data.SqlServer;

public sealed class SqlServerStoredProcedureHandler : IStoredProcedureHandler
{
    public const int CommandTimeout = 10;
    private readonly SqlServerSession _session;

    public SqlServerStoredProcedureHandler(SqlServerSession session)
    {
        _session = session;
    }

    public async Task<IEnumerable<T1>> ExecuteListAsync<T1>(string procedureName, ListProcedureParameters procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_LST_")) throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteListAsync)}. It should start with SP_LST_<ProcName>.");

        var procedureResult = await _session.Connection.QueryAsync<T1>(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeout);

        return procedureResult;
    }

    public async Task<T1> ExecuteGetAsync<T1>(string procedureName, GetProcedureParameters procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_GET_")) throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteGetAsync)}. It should start with SP_GET_<ProcName>.");

        var procedureResult = await _session.Connection.QuerySingleAsync<T1>(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeout);

        return procedureResult;
    }

    public async Task<int> ExecuteAddAsync(string procedureName, AddProcedureParameters procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_ADD_")) throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteAddAsync)}. It should start with SP_ADD_<ProcName>.");

        var procedureResult = await _session.Connection.ExecuteAsync(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeout);

        return procedureResult;
    }

    public async Task<int> ExecuteUpdateAsync(string procedureName, UpdateProcedureParameters procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_UPD_")) throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteUpdateAsync)}. It should start with SP_UPD_<ProcName>.");

        var procedureResult = await _session.Connection.ExecuteAsync(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeout);

        return procedureResult;
    }

    public async Task<int> ExecuteDeleteAsync(string procedureName, DeleteProcedureParameters procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_DEL_")) throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteDeleteAsync)}. It should start with SP_DEL_<ProcName>.");

        var procedureResult = await _session.Connection.ExecuteAsync(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeout);

        return procedureResult;
    }
}
