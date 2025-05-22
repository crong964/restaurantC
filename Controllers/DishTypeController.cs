using System.Threading.Tasks;
using _netmvc.Data;
using _netmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _netmvc.Controllers;

public class DishTypeController : Controller
{
    [BindProperty]
    public required DishTypeCreate DishTypeCreate { get; set; }
    private readonly MvcMovieContext _context;
    public DishTypeController(MvcMovieContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        return View(await _context.DishType.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DishTypeCreate dishTypeCreate)
    {
        if (ModelState.IsValid)
        {
            _context.Add(new DishType
            {
                Name = dishTypeCreate.Name,
                Number = 10
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }
        var dishType = await _context.DishType.FindAsync(id);
        if (dishType == null)
        {
            return RedirectToAction("Index");
        }
        return View(new DishTypeCreate
        {
            Name = dishType.Name,
            Id = dishType.Id,
            Number = dishType.Number
        });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DishTypeCreate? dishTypeCreate)
    {
        if (dishTypeCreate == null)
        {
            return RedirectToAction("Index");
        }
        var dishType = await _context.DishType.FindAsync(dishTypeCreate.Id);

        if (dishType == null)
        {
            return RedirectToAction("Index");
        }

        dishType.Name = dishTypeCreate.Name;
        dishType.Number = dishTypeCreate.Number;
        _context.DishType.Update(dishType);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
