using Dapper;

using System.Data;

using TechBox.Api.Data.Dto;

namespace TechBox.Api.Data.SqlServer
{
    public sealed class SqlServerStoredProcedureHandler : IStoredProcedureHandler
    {
        public readonly int CommandTimeout = 300;

        private readonly SqlServerSession _session;

        public SqlServerStoredProcedureHandler(SqlServerSession session)
        {
            _session = session;
        }

        public IEnumerable<T1> ExecuteList<T1>(string procedureName, ListProcedureDto procedureParameter, int? commandTimeout = null)
        {
            using (_session)
            {
                var procedureResult = _session.Connection.Query<T1>(
                    procedureName,
                    param: procedureParameter,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: commandTimeout);

                return procedureResult;
            }
        }
    }
}
