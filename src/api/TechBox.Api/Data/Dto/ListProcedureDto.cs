namespace TechBox.Api.Data.Dto
{
    public class ListProcedureDto
    {
        private int? _pageNumber;
        public int? PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value <= 0 ? 1 : value;
            }
        }

        private int? _pageSize;
        public int? PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value <= 0 ? 1 : value;
            }
        }
    }
}
