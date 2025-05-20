using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _netmvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace MvcMovie.Controllers;

[Authorize(Policy = "Em")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDateTime _dateTime;
    public HomeController(ILogger<HomeController> logger, IDateTime dateTime)
    {
        _logger = logger;
        _dateTime = dateTime;
    }

    public IActionResult Index()
    {
        _logger.Log(LogLevel.Error, "home");
        Console.WriteLine(_dateTime.Now);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
