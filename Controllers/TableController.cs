using System.Threading.Tasks;
using _netmvc.Data;
using _netmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _netmvc.Controllers;

public class TableController : Controller
{
    private readonly MvcMovieContext _content;
    public TableController(MvcMovieContext context)
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

    [HttpPost]
    public async Task<IActionResult> Create(int number)
    {

        var table = new Table
        {
            number = number,
            status = "empty"
        };
        _content.Table.Add(table);
        await _content.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int Id)
    {
        var table = _content.Table.Find(Id);

        if (table != null)
        {
            _content.Table.Remove(table);
            await _content.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}
