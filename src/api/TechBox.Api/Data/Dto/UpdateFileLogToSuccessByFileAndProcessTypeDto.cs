using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToSuccessByFileAndProcessTypeDto : UpdateProcedureParameters
{
    public DateTime FinishedAt { get; set; }
    public ProcessTypesEnum ProcessTypeId { get; set; }

    public UpdateFileLogToSuccessByFileAndProcessTypeDto(Guid fileId, ProcessTypesEnum processTypeId) : base(fileId)
    {
        FinishedAt = DateTime.UtcNow;
        ProcessTypeId = processTypeId;
    }
}
