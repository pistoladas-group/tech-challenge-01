namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToFailedByFileIdAndProcessTypeDto : UpdateProcedureParameters
{
    public DateTime FinishedAt { get; set; }
    public string ErrorMessage { get; set; }
    
    public UpdateFileLogToFailedByFileIdAndProcessTypeDto(Guid fileId, string errorMessage) : base(fileId)
    {
        FinishedAt = DateTime.UtcNow;
        ErrorMessage = errorMessage;
    }
}
