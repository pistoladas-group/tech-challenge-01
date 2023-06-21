using System.Net;
using Microsoft.AspNetCore.Mvc;
using TechBox.Api.Data;
using TechBox.Api.Data.Dto;
using TechBox.Api.Models;
using TechBox.Api.Services;

namespace TechBox.Api.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly ILogger<FilesController> _logger;
    private readonly IFileRepository _fileRepository;
    private readonly IFileStorageService _fileStorageService;

    public FilesController(ILogger<FilesController> logger, IFileRepository fileRepository, IFileStorageService fileStorageService)
    {
        _logger = logger;
        _fileRepository = fileRepository;
        _fileStorageService = fileStorageService;
    }

    /// <summary>
    /// Get all files
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="500">An internal error occurred</response>
    [HttpGet("")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllFiles()
    {
        var storedFiles = await _fileRepository.ListFilesAsync(1, 10);

        return Ok(new ApiResponse(data: storedFiles));
    }

    /// <summary>
    /// Get file by id
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="404">The resource was not found</response>
    /// <response code="500">An internal error occurred</response>
    [HttpGet("{fileId:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetFileById([FromRoute] Guid fileId)
    {
        if (fileId == Guid.Empty)
        {
            return BadRequest(new ApiResponse());
        }

        var storedFile = await _fileRepository.GetFileByIdAsync(fileId);

        if (storedFile is null)
        {
            return NotFound(new ApiResponse());
        }

        return Ok(new ApiResponse(data: storedFile));
    }

    /// <summary>
    /// Upload a file
    /// </summary>
    /// <response code="201">Returns the created resource endpoint in response header</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="500">An internal error occurred</response>
    [HttpPost("")]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UploadFile(IFormFile formFile)
    {
        var isAValidFile = _fileStorageService.ValidateFile(formFile);

        if (!isAValidFile)
        {
            return BadRequest(new ApiResponse(error: "")); //TODO: return error sent by the service
        }

        var fileId = await _fileRepository.AddFileAsync(new AddFileDto
        {
            Name = formFile.FileName,
            SizeInBytes = (int)formFile.Length,
            ProcessStatusId = ProcessStatusEnum.Pending
        });

        await _fileStorageService.UploadFileAsync(formFile);

        return CreatedAtAction(nameof(GetFileById), new { fileId }, new ApiResponse());
    }

    /// <summary>
    /// Delete a file
    /// </summary>
    /// <response code="202">Operation accepted and being processed</response>
    /// <response code="404">The resource was not found</response>
    /// <response code="500">An internal error occurred</response>
    [HttpDelete("{fileId:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Accepted)] //TODO: https://www.rfc-editor.org/rfc/rfc9110.html#section-9.3.5
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteFile([FromRoute] Guid fileId)
    {
        var file = await _fileRepository.GetFileByIdAsync(fileId);

        if (file is null)
        {
            return NotFound(new ApiResponse("no file found"));
        }

        // TODO: "Marcado" para remover, agora outro processo se encarregar� de processar

        var fileLogId = await _fileRepository.AddFileLogAsync(new AddFileLogDto()
        {
            FileId = fileId,
            ProcessStatusId = ProcessStatusEnum.Pending,
            ProcessTypeId = ProcessTypesEnum.Delete
        });


        // TODO: #### Begin Background ####

        await _fileRepository.UpdateFileLogToProcessingByIdAsync(fileLogId);

        await _fileStorageService.DeleteFileAsync(file.Name);

        // TODO: Se sucesso:
        //              - Atualizar Files (Url = NULL, IsDeleted = 1, ProcessStatusId = Success)
        //              - Atualizar FileLogs (ProcessStatusId = Success, FinishedAt = DateTime.UtcNow)
        await _fileRepository.UpdateFileLogToSuccessByIdAsync(fileLogId);

        await _fileRepository.DeleteFileByIdAsync(fileId);

        // TODO: Se erro:
        //              - Atualizar Files (ProcessStatusId = Failed)
        //              - Atualizar FileLogs (ProcessStatusId = Failed, FinishedAt = DateTime.UtcNow, ErrorMessage = "<error>")
        // await _fileRepository.UpdateFileLogToFailedByIdAsync(fileLogId, "An error ocurred");

        // TODO: #### End Background ####

        return Accepted(new ApiResponse());
    }
}

//TODO: https://github.com/serilog/serilog/wiki/Writing-Log-Events
//TODO: https://github.com/serilog/serilog/wiki/Provided-Sinks
