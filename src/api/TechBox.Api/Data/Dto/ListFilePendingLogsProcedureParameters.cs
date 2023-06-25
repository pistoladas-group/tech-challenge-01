namespace TechBox.Api.Data.Dto;

public class ListFilePendingLogsProcedureParameters : ListProcedureParameters
{
    public Guid FileId { get; set; }

    public ListFilePendingLogsProcedureParameters(Guid fileId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
        FileId = fileId;
    }
}
