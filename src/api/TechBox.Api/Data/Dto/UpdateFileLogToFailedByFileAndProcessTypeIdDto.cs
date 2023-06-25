using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToFailedByFileAndProcessTypeIdDto : UpdateProcedureParameters
{
    public ProcessTypesEnum ProcessTypeId { get; set; }
    public DateTime FinishedAt { get; set; }
    public string ErrorMessage { get; set; }

    public UpdateFileLogToFailedByFileAndProcessTypeIdDto(Guid fileId, ProcessTypesEnum processTypeId, string errorMessage) : base(fileId)
    {
        ProcessTypeId = processTypeId;
        FinishedAt = DateTime.UtcNow;
        ErrorMessage = errorMessage;
    }
}
