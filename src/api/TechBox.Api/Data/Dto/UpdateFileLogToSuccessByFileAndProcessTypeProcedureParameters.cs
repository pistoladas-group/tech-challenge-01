using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToSuccessByFileAndProcessTypeProcedureParameters : UpdateProcedureParameters
{
    public DateTime FinishedAt { get; set; }
    public ProcessTypesEnum ProcessTypeId { get; set; }

    public UpdateFileLogToSuccessByFileAndProcessTypeProcedureParameters(Guid fileId, ProcessTypesEnum processTypeId) : base(fileId)
    {
        FinishedAt = DateTime.UtcNow;
        ProcessTypeId = processTypeId;
    }
}
