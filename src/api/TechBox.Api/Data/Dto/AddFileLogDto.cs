using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class AddFileLogDto : AddProcedureParameters
{
    public Guid FileId { get; set; }
    public ProcessStatusEnum ProcessStatusId { get; set; }
    public ProcessTypesEnum ProcessTypeId { get; set; }

    public AddFileLogDto(Guid? id, Guid fileId, ProcessTypesEnum processTypeId) : base(id)
    {
        FileId = fileId;
        ProcessStatusId = ProcessStatusEnum.Pending;
        ProcessTypeId = processTypeId;
    }
}
