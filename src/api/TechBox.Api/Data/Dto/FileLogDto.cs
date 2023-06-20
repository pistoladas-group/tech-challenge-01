using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class FileLogDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid FileId { get; set; }
    public ProcessStatusEnum ProcessStatusId { get; set; }
    public ProcessTypesEnum ProcessTypeEnum { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}
