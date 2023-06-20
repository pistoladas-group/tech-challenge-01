using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileLogToFailedByIdDto : UpdateProcedureParameters
{
    public ProcessStatusEnum ProcessStatusId { get; set; }
    public DateTime FinishedAt { get; set; }
    public string ErrorMessage { get; set; } = "Unknown Error";

    public UpdateFileLogToFailedByIdDto(Guid id) : base(id)
    {
    }
}
