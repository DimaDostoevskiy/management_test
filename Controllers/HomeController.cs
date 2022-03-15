using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace asu_management.mvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
