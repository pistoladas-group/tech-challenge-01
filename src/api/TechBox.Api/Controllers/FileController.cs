using System.Net;

using Microsoft.AspNetCore.Mvc;

using TechBox.Api.Models;

namespace TechBox.Api.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly ILogger<FilesController> _logger;

    public FilesController(ILogger<FilesController> logger)
    {
        _logger = logger;
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
        return Ok(new ApiResponse());
    }
}
