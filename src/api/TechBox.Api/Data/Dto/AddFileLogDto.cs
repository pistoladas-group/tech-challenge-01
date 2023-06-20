using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class AddFileLogDto : AddProcedureParameters
{
    public Guid FileId { get; set; }
    public ProcessStatusEnum ProcessStatusId { get; set; }
    public ProcessTypesEnum ProcessTypeId { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? StartedAt { get; set; }
    public string? FinishedAt { get; set; }
}
