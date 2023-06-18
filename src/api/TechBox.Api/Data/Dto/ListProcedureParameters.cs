namespace TechBox.Api.Data.Dto;

public class ListProcedureParameters
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public ListProcedureParameters(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;
        PageSize = pageNumber <= 0 ? 1 : pageSize;
    }
}
