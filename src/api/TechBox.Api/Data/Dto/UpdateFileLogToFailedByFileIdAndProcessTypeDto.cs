using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToFailedByFileIdAndProcessTypeDto : UpdateProcedureParameters
{
    public DateTime FinishedAt { get; set; }
    public ProcessTypesEnum ProcessTypesId { get; set; }
    public string ErrorMessage { get; set; }
    
    public UpdateFileLogToFailedByFileIdAndProcessTypeDto(Guid fileId, ProcessTypesEnum processTypesId, string errorMessage) : base(fileId)
    {
        FinishedAt = DateTime.UtcNow;
        ProcessTypesId = processTypesId;
        ErrorMessage = errorMessage;
    }
}
