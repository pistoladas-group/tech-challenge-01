using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class UpdateFileProcessStatusByIdProcedureParameters : UpdateProcedureParameters
{
    public UpdateFileProcessStatusByIdProcedureParameters(Guid id) : base(id)
    {
    }

    public ProcessStatusEnum ProcessStatusId { get; set; }
}
