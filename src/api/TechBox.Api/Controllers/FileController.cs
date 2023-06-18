using System.Net;
using Microsoft.AspNetCore.Mvc;
using TechBox.Api.Configurations;
using TechBox.Api.Data;
using TechBox.Api.Models;

namespace TechBox.Api.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly ILogger<FilesController> _logger;
    private readonly IFileRepository _fileRepository;

    public FilesController(ILogger<FilesController> logger, IFileRepository fileRepository)
    {
        _logger = logger;
        _fileRepository = fileRepository;
    }

    /// <summary>
    /// Get all files
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="500">There was an internal problem</response>
    [HttpGet("")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllFiles()
    {
        //TODO: Remover
        var teste = _fileRepository.ListFiles(1, 10);

        return Ok(new ApiResponse(data: teste));
    }
    
    /// <summary>
    /// Test
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="500">There was an internal problem</response>
    [HttpGet("test")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Test()
    {
        var teste = Environment.GetEnvironmentVariable(EnvironmentVariables.Chavinha);

        return Ok(new ApiResponse(data: teste));
    }
}