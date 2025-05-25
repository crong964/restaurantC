using System.Threading.Tasks;
using _netmvc.Data;
using _netmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _netmvc.Controllers;

public class OrderDetail : Controller
{
    private readonly MvcMovieContext _content;
    public OrderDetail(MvcMovieContext context)
    {
        _content = context;
    }

    public async Task<IActionResult> Index(string? status)
    {
        var l = await _content.Table.OrderBy(i => i.Id).LastOrDefaultAsync();
        if (l != null)
        {
            ViewData["numbertable"] = l.number + 1;
        }
        return View(await _content.Table.ToArrayAsync());
    }
}
