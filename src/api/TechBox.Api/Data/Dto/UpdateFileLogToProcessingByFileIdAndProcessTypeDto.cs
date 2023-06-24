using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToProcessingByFileIdAndProcessTypeDto : UpdateProcedureParameters
{
    public DateTime StartedAt { get; set; }
    public ProcessTypesEnum ProcessTypeId { get; set; }
    
    public UpdateFileLogToProcessingByFileIdAndProcessTypeDto(Guid fileId, ProcessTypesEnum processTypeId) : base(fileId)
    {
        StartedAt = DateTime.UtcNow;
        ProcessTypeId = processTypeId;
    }
}
