namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToProcessingByFileIdAndProcessTypeDto : UpdateProcedureParameters
{
    public DateTime StartedAt { get; set; }
    
    public UpdateFileLogToProcessingByFileIdAndProcessTypeDto(Guid fileId) : base(fileId)
    {
        StartedAt = DateTime.UtcNow;
    }
}
