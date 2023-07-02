namespace TechBox.Api.Data.Dto;

public class UpdateFileByIdProcedureParameters : UpdateProcedureParameters
{
    public string? Url { get; set; }
    
    public UpdateFileByIdProcedureParameters(Guid fileId, Uri? url) : base(fileId)
    {
        Url = url?.ToString();
    }
}
