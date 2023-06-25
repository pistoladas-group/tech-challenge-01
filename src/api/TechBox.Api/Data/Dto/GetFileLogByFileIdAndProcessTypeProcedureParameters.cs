using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class GetFileLogByFileIdAndProcessTypeProcedureParameters : GetProcedureParameters
{
    public ProcessTypesEnum ProcessTypeId { get; init; }

    public GetFileLogByFileIdAndProcessTypeProcedureParameters(Guid id, ProcessTypesEnum processTypeId) : base(id)
    {
        ProcessTypeId = processTypeId;
    }
}
