using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToProcessingByFileAndProcessTypeDto : UpdateProcedureParameters
{
    public ProcessTypesEnum ProcessTypeId { get; set; }
    public DateTime StartedAt { get; set; }

    public UpdateFileLogToProcessingByFileAndProcessTypeDto(Guid fileId, ProcessTypesEnum processTypeId) : base(fileId)
    {
        ProcessTypeId = processTypeId;
        StartedAt = DateTime.UtcNow;
    }
}
