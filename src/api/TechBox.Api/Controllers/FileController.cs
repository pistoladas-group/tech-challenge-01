using System.Net;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using TechBox.Api.Configurations;
using TechBox.Api.Data;
using TechBox.Api.Data.Dto;
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
        var fileNameSplit = formFile.FileName.Split(".");
        
        var fileId = await _fileRepository.AddFileAsync(new AddFileDto()
        {
            Name = fileNameSplit[0],
            Extension = fileNameSplit[1].ToLower(), // TODO: Somente arquivos com extens�o de imagens
            SizeInBytes = (int)formFile.Length, // TODO: Limitar tamanho do arquivo
            ProcessStatusId = ProcessStatusEnum.Pending
        });

        //TODO: mudar status para processing e criar serviço de processamento (serviço deve validar variáveis de ambiente)
        //TODO: gravar o tipo de processamento como Inclusão
        var blobServiceClient = new BlobServiceClient(
            new Uri(Environment.GetEnvironmentVariable(EnvironmentVariables.StorageAccountUrl)),
            new DefaultAzureCredential());

        var containerClient = blobServiceClient.GetBlobContainerClient(Environment.GetEnvironmentVariable(EnvironmentVariables.StorageAccountContainerName));
        var blobClient = containerClient.GetBlobClient(formFile.FileName);

        //TODO: aqui pode dar erro.
        //TODO: tentar passar o content type
        await using (var stream = formFile.OpenReadStream())
        {
            await blobClient.UploadAsync(stream);
        }

        //TODO: mudar status para sucesso ou erro, atualizar url
        return CreatedAtAction(nameof(GetFileById), new { fileId }, new ApiResponse());
    }

    /// <summary>
    /// Delete a file
    /// </summary>
    /// <response code="204">Operation succeeded with no response</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="404">The resource was not found</response>
    /// <response code="500">An internal error occurred</response>
    [HttpDelete("{fileId:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteFile([FromRoute] Guid fileId)
    {
        //TODO: gravar o tipo de processamento como exclusão

        return Ok(new ApiResponse());
    }
}