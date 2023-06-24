using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToFailedByIdDto : UpdateProcedureParameters
{
    public ProcessStatusEnum ProcessStatusId { get; set; }
    public DateTime FinishedAt { get; set; }
    public string ErrorMessage { get; set; }
    
    public UpdateFileLogToFailedByIdDto(Guid id, string errorMessage) : base(id)
    {
        ProcessStatusId = ProcessStatusEnum.Failed;
        FinishedAt = DateTime.UtcNow;
        ErrorMessage = errorMessage;
    }
}
