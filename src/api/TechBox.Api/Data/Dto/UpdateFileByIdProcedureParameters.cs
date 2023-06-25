namespace TechBox.Api.Data.Dto;

public class UpdateFileByIdProcedureParameters : UpdateProcedureParameters
{
    public string? Url { get; set; }
    public bool IsDeleted { get; set; }
    
    public UpdateFileByIdProcedureParameters(Guid fileId, Uri? url, bool isDeleted) : base(fileId)
    {
        Url = url?.ToString();
        IsDeleted = isDeleted;
    }
}
