namespace TechBox.Api.Data.Dto;

public class GetProcedureParameters
{
    public Guid Id { get; init; }

    public GetProcedureParameters(Guid id)
    {
        // TODO: Isso aqui não deixaria o Id vazio do mesmo jeito?
        // Porq se não entrar no IF o default de Guid é um Guid.Empty mesmo
        if (id != Guid.Empty)
        {
            Id = id;
        }
    }
}
