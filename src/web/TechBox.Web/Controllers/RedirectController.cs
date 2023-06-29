using Microsoft.AspNetCore.Mvc;

namespace TechBox.Web.Controllers;

[Route("")]
public class RedirectController : Controller
{
    public RedirectController()
    {
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Home");
    }
}