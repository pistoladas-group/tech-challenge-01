﻿using TechBox.Api.Models;

namespace TechBox.Api.Data.Dto;

public class AddFileDto : AddProcedureParameters
{
    public string Name { get; set; }
    public long SizeInBytes { get; set; }
    public string ContentType { get; set; }
    public ProcessStatusEnum ProcessStatusId { get; set; }
    
    public AddFileDto(string name, long sizeInBytes, string contentType)
    {
        Name = name;
        SizeInBytes = sizeInBytes;
        ContentType = contentType;
        ProcessStatusId = ProcessStatusEnum.Pending;
    }
}
