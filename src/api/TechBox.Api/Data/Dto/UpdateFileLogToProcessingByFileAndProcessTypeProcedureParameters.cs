using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToProcessingByFileAndProcessTypeProcedureParameters : UpdateProcedureParameters
{
    public ProcessTypesEnum ProcessTypeId { get; set; }
    public DateTime StartedAt { get; set; }

    public UpdateFileLogToProcessingByFileAndProcessTypeProcedureParameters(Guid fileId, ProcessTypesEnum processTypeId) : base(fileId)
    {
        ProcessTypeId = processTypeId;
        StartedAt = DateTime.UtcNow;
    }
}
