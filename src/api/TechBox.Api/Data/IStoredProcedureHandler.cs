using TechBox.Api.Data.Dto;

namespace TechBox.Api.Data
{
    public interface IStoredProcedureHandler
    {
        IEnumerable<T1> ExecuteList<T1>(string procedureName, ListProcedureDto procedureParameter, int? commandTimeout = null);
    }
}
