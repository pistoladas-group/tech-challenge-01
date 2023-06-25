namespace TechBox.Api.Data.Dto;

public class GetProcedureParameters
{
    public Guid Id { get; init; }

    public GetProcedureParameters(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException($"Invalid parameter: {nameof(id)}");
        }

        Id = id;
    }
}
