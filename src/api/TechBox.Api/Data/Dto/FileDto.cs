using System.Text.Json.Serialization;
using TechBox.Api.Common;
using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class FileDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    
    [JsonConverter(typeof(JsonUtcDateTimeFormatter))]
    public DateTime CreatedAt { get; set; }

    public string Name { get; set; }
    public int SizeInBytes { get; set; }
    public string? Url { get; set; }
    public string ContentType { get; set; }
    public ProcessStatusEnum ProcessStatusId { get; set; }
}
