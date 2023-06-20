using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToProcessingByIdDto : UpdateProcedureParameters
{
    public UpdateFileLogToProcessingByIdDto(Guid id) : base(id)
    {
    }

    public ProcessStatusEnum ProcessStatusId { get; set; }
    public DateTime StartedAt { get; set; }
}
