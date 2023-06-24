namespace TechBox.Api.Data.Dto;

public class AddProcedureParameters
{
    public Guid Id { get; init; }
    public bool IsDeleted { get; init; }
    public DateTime CreatedAt { get; init; }

    protected AddProcedureParameters(Guid? id)
    {
        Id = id ?? Guid.NewGuid();
        IsDeleted = false;
        CreatedAt = DateTime.UtcNow;
    }
}
