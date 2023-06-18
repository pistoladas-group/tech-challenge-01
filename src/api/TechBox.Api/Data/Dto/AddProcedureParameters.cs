namespace TechBox.Api.Data.Dto;

public class AddProcedureParameters
{
    public Guid Id { get; init; }
    public bool IsDeleted { get; init; }
    public DateTime CreatedAt { get; init; }

    public AddProcedureParameters()
    {
        Id = Guid.NewGuid();
        IsDeleted = false;
        CreatedAt = DateTime.UtcNow;
    }
}
