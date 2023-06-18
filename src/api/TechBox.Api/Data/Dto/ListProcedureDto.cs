namespace TechBox.Api.Data.Dto
{
    public class ListProcedureDto
    {
        public int PageNumber { get; }

        public int PageSize { get; }

        public ListProcedureDto(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageNumber <= 0 ? 1 : pageSize;
        }
    }
}
