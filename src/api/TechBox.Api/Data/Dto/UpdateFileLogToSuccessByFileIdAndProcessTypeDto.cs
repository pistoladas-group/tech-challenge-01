using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToSuccessByFileIdAndProcessTypeDto : UpdateProcedureParameters
{
    public DateTime FinishedAt { get; set; }
    public ProcessTypesEnum ProcessTypeId { get; set; }

    public UpdateFileLogToSuccessByFileIdAndProcessTypeDto(Guid fileId, ProcessTypesEnum processTypeId) : base(fileId)
    {
        FinishedAt = DateTime.UtcNow;
        ProcessTypeId = processTypeId;
    }
}
