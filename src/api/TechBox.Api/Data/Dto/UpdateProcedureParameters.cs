namespace TechBox.Api.Data.Dto;

public class UpdateProcedureParameters
{
    public Guid FileId { get; init; }

    public UpdateProcedureParameters(Guid fileId)
    {
        FileId = fileId;
    }
}
