using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using TechBox.Web.Models;

namespace TechBox.Web.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
