using System.Data;
using System.Data.SqlClient;

namespace TechBox.Api.Data.SqlServer
{
    public sealed class SqlServerSession : IDisposable
    {
        public IDbConnection Connection { get; }

        public SqlServerSession(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
