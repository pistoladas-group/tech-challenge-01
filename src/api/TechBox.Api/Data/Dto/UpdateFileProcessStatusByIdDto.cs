using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileProcessStatusByIdDto : UpdateProcedureParameters
{
    public UpdateFileProcessStatusByIdDto(Guid id) : base(id)
    {
    }

    public ProcessStatusEnum ProcessStatusId { get; set; }
}
