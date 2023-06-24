using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToSuccessByFileIdAndProcessTypeDto : UpdateProcedureParameters
{
    public DateTime FinishedAt { get; set; }
    public ProcessTypesEnum ProcessTypesId { get; set; }

    public UpdateFileLogToSuccessByFileIdAndProcessTypeDto(Guid fileId, ProcessTypesEnum processTypesId) : base(fileId)
    {
        ProcessTypesId = processTypesId;
        FinishedAt = DateTime.UtcNow;
    }
}
