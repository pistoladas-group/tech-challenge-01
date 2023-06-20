using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToSuccessByIdDto : UpdateProcedureParameters
{
    public ProcessStatusEnum ProcessStatusId { get; set; }
    public DateTime FinishedAt { get; set; }

    public UpdateFileLogToSuccessByIdDto(Guid id) : base(id)
    {
    }
}
