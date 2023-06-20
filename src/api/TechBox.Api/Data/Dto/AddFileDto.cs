using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class AddFileDto : AddProcedureParameters
{
    public string Name { get; set; }
    public int SizeInBytes { get; set; }
    public string? Url { get; set; }
    public ProcessStatusEnum ProcessStatusId { get; set; }
}
