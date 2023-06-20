namespace TechBox.Api.Data.Dto;

public class DeleteProcedureParameters
{
    public Guid Id { get; init; }

    public DeleteProcedureParameters(Guid id)
    {
        Id = id;
    }
}
