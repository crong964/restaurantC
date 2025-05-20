using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Encodings.Web;

namespace _netmvc.Controllers;

public class HelloWorldController : Controller
{
    private readonly IMemoryCache _memoryCache;
    public HelloWorldController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    // 
    // GET: /HelloWorld/
    public IActionResult Index()
    {
        
        _memoryCache.Set("huy", "memoryCache", new MemoryCacheEntryOptions
        {
            Priority=CacheItemPriority.NeverRemove
        });
        Response.Cookies.Append("name", "huydkjask", new CookieOptions()
        {
            MaxAge = new TimeSpan(1, 1, 1)
        });
        return View();
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    public IActionResult Welcome(string name, int numTimes = 1)
    {

        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;
        ViewData["title"] = _memoryCache.Get("huy");
        return View();

    }
}