using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

namespace TechBox.Web.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    // TODO: Tratar exceções em um filtro retornando
    // um JSON para o front similiar ao ApiResponse
    // para que o JS não receba a exceção "pura"

    private readonly IHttpClientFactory _httpFactory;

    public HomeController(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFileAsync(IFormFile formFile)
    {
        var client = _httpFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:5001/api/files"); //TODO: deixar a url dinâmica
        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(formFile.OpenReadStream());

        streamContent.Headers.Add("Content-Type", formFile.ContentType);

        content.Add(streamContent, "formFile", formFile.FileName);

        request.Content = content;

        var apiResponse = await client.SendAsync(request);

        apiResponse.EnsureSuccessStatusCode();

        apiResponse.Headers.TryGetValues("location", out var locationValues);

        request = new HttpRequestMessage(HttpMethod.Get, locationValues?.FirstOrDefault());

        apiResponse = await client.SendAsync(request);

        apiResponse.EnsureSuccessStatusCode();

        var reponse = JsonSerializer.Serialize(await apiResponse.Content.ReadAsStringAsync());

        return Ok(reponse);
    }

    [HttpGet("files")]
    public async Task<IActionResult> ListFilesAsync()
    {
        var client = _httpFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:5001/api/files?pageNumber=1&pageSize=100"); //TODO: deixar a url dinâmica

        var apiResponse = await client.SendAsync(request);

        apiResponse.EnsureSuccessStatusCode();

        var reponse = JsonSerializer.Serialize(await apiResponse.Content.ReadAsStringAsync());

        return Ok(reponse);
    }

    [HttpPost("{fileId:guid}/status")]
    public IActionResult GetFileStatus([FromRoute] Guid fileId)
    {
        return Ok();
    }
}
