namespace TechBox.Api.Data.Dto;

public class UpdateProcedureParameters
{
    public Guid Id { get; init; }

    public UpdateProcedureParameters(Guid id)
    {
        Id = id;
    }
}
