using System.Data;
using Dapper;
using TechBox.Api.Data.Dto;

namespace TechBox.Api.Data.SqlServer;

public sealed class SqlServerStoredProcedureHandler : IStoredProcedureHandler
{
    private const int CommandTimeoutInSeconds = 10;
    private readonly SqlServerSession _session;

    public SqlServerStoredProcedureHandler(SqlServerSession session)
    {
        _session = session;
    }

    public async Task<IEnumerable<T1>> ExecuteListAsync<T1>(string procedureName, object procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_LST_"))
        {
            throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteListAsync)}. It should start with SP_LST_<ProcName>.");
        }

        var procedureResult = await _session.Connection.QueryAsync<T1>(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeoutInSeconds);

        return procedureResult;
    }

    public async Task<T1> ExecuteGetAsync<T1>(string procedureName, object procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_GET_"))
        {
            throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteGetAsync)}. It should start with SP_GET_<ProcName>.");
        }

        var procedureResult = await _session.Connection.QuerySingleAsync<T1>(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeoutInSeconds);

        return procedureResult;
    }

    public async Task<int> ExecuteAddAsync(string procedureName, object procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_ADD_"))
        {
            throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteAddAsync)}. It should start with SP_ADD_<ProcName>.");
        }

        var procedureResult = await _session.Connection.ExecuteAsync(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeoutInSeconds);

        return procedureResult;
    }

    public async Task<int> ExecuteUpdateAsync(string procedureName, object procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_UPD_"))
        {
            throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteUpdateAsync)}. It should start with SP_UPD_<ProcName>.");
        }

        var procedureResult = await _session.Connection.ExecuteAsync(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeoutInSeconds);

        return procedureResult;
    }

    public async Task<int> ExecuteDeleteAsync(string procedureName, object procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_DEL_"))
        {
            throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteDeleteAsync)}. It should start with SP_DEL_<ProcName>.");
        }

        var procedureResult = await _session.Connection.ExecuteAsync(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeoutInSeconds);

        return procedureResult;
    }
    
    public async Task<bool> ExecuteCheckAsync(string procedureName, object procedureParameter, int? commandTimeout = null)
    {
        if (!procedureName.StartsWith("SP_CHK_"))
        {
            throw new ArgumentException($"The procedure name {procedureName} is invalid for {nameof(ExecuteCheckAsync)}. It should start with SP_CHK_<ProcName>.");
        }

        var procedureResult = await _session.Connection.QuerySingleAsync<bool>(
            procedureName,
            param: procedureParameter,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout ?? CommandTimeoutInSeconds);

        return procedureResult;
    }
}
