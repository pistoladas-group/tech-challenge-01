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
    private readonly IFileRepository _fileRepository;
    private readonly IRemoteFileStorageService _remoteFileStorageService;
    private readonly ILocalFileStorageService _localFileStorageService;

    public FilesController(IFileRepository fileRepository, IRemoteFileStorageService remoteFileStorageService, ILocalFileStorageService localFileStorageService)
    {
        _fileRepository = fileRepository;
        _remoteFileStorageService = remoteFileStorageService;
        _localFileStorageService = localFileStorageService;
    }

    /// <summary>
    /// Get all files
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="500">An internal error occurred</response>
    [HttpGet("")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllFiles([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest(new ApiResponse(error: "page number and page size must be greater than 0."));
        }
        
        var storedFiles = await _fileRepository.ListFilesAsync(pageNumber, pageSize);

        return Ok(new ApiResponse(data: storedFiles));
    }

    /// <summary>
    /// Get a file by id
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="404">The resource was not found</response>
    /// <response code="500">An internal error occurred</response>
    [HttpGet("{id:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetFileById([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse(error: "Invalid fileId"));
        }

        var storedFile = await _fileRepository.GetFileByIdAsync(id);

        if (storedFile is null)
        {
            return NotFound(new ApiResponse(error: "File not found"));
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
        var result = _remoteFileStorageService.ValidateFile(formFile);

        if (!result.IsSuccess)
        {
            return BadRequest(new ApiResponse(result.Errors));
        }
        
        var fileToAdd = new AddFileDto(formFile.FileName, formFile.Length, formFile.ContentType);
        
        var id = await _fileRepository.AddFileAsync(fileToAdd);

        var existingFileLog = await _fileRepository.GetFileLogByFileIdAndProcessTypeIdAsync(id, ProcessTypesEnum.Upload);
        
        if (existingFileLog is null)
        {
            await _fileRepository.AddFileLogAsync(new AddFileLogDto(id, ProcessTypesEnum.Upload));
        }

        _localFileStorageService.SaveFile(formFile, id);

        return CreatedAtAction(nameof(GetFileById), new { id }, new ApiResponse(data: new { id }));
    }

    /// <summary>
    /// Delete a file by id
    /// </summary>
    /// <response code="202">Operation accepted and being processed</response>
    /// <response code="404">The resource was not found</response>
    /// <response code="500">An internal error occurred</response>
    [HttpDelete("{id:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteFile([FromRoute] Guid id)
    {
        var file = await _fileRepository.GetFileByIdAsync(id);

        if (file is null)
        {
            return NotFound(new ApiResponse(error: "no file found"));
        }

        await _fileRepository.UpdateFileProcessStatusByIdAsync(id, ProcessStatusEnum.Pending);

        var existingFileLog = await _fileRepository.GetFileLogByFileIdAndProcessTypeIdAsync(id, ProcessTypesEnum.Delete);
        
        if (existingFileLog is null)
        {
            await _fileRepository.AddFileLogAsync(new AddFileLogDto(id, ProcessTypesEnum.Delete));
        }
        
        return Accepted(new ApiResponse());
    }
}
