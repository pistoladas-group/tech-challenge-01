using Microsoft.AspNetCore.Mvc;

namespace TechBox.Web.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    public HomeController()
    {
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFileAsync(IFormFile formFile)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:5001/api/files"); //TODO: deixar a url dinâmica
        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(formFile.OpenReadStream());

        streamContent.Headers.Add("Content-Type", formFile.ContentType);
        
        content.Add(streamContent, "formFile", formFile.FileName);
        
        request.Content = content;
        
        var apiResponse = await client.SendAsync(request);
        
        apiResponse.EnsureSuccessStatusCode();

        var reponse = System.Text.Json.JsonSerializer.Serialize(await apiResponse.Content.ReadAsStringAsync());
        
        Console.WriteLine(await apiResponse.Content.ReadAsStringAsync());
        return Ok(reponse);
    }
    
    [HttpPost("{fileId:guid}/status")]
    public IActionResult GetFileStatus([FromRoute] Guid fileId)
    {
        return Ok();
    }
}
