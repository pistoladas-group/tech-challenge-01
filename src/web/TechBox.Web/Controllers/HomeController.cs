using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

namespace TechBox.Web.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
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

        if (!apiResponse.IsSuccessStatusCode && apiResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        {
            var errorResponse = JsonSerializer.Serialize(await apiResponse.Content.ReadAsStringAsync());
            return new ObjectResult(errorResponse)
            {
                StatusCode = 500
            };
        }

        if (!apiResponse.IsSuccessStatusCode && apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            var badResponse = JsonSerializer.Serialize(await apiResponse.Content.ReadAsStringAsync());
            return BadRequest(badResponse);
        }

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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> GetFileStatus([FromRoute] Guid id)
    {
        var client = _httpFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:5001/api/files/{id}"); //TODO: deixar a url dinâmica
        var apiResponse = await client.SendAsync(request);

        apiResponse.EnsureSuccessStatusCode();
        
        //TODO: seguir daqui
        
        return Ok();
    }
}
