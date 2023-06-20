using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileProcessStatusByIdDto : UpdateProcedureParameters
{
    public ProcessStatusEnum ProcessStatusId { get; set; }

    public UpdateFileProcessStatusByIdDto(Guid id) : base(id)
    {
    }
}
