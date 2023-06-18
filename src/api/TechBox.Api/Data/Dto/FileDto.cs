namespace TechBox.Api.Data.Dto;

public class FileDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public int SizeInBytes { get; set; }
    public string? Url { get; set; }
    public byte ProcessStatusId { get; set; }
}
