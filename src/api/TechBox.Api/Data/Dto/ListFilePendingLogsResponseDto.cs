using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class ListFilePendingLogsResponseDto
{
    public Guid FileId {get; set; }
    public string FileName {get; set; }
    public string FileContentType {get; set; }
    public ProcessTypesEnum ProcessTypeId {get; set; }
}