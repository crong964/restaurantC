using System.Threading.Tasks;
using _netmvc.Data;
using _netmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _netmvc.Controllers;

public class DishController : Controller
{
    [BindProperty]
    public required DishCreate DishCreate { get; set; }
    private readonly MvcMovieContext _context;
    public DishController(MvcMovieContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        ViewData["ima"] = "https://localhost:7019/sta/638833544375746922.jpg";
        return View(await _context.Dish.ToListAsync());
    }

    public async Task<IActionResult> Create()
    {

        return View(new DishView
        {
            DishTypes = await _context.DishType.ToListAsync()
        });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestSizeLimit(100_000_000_000_000)]
    public async Task<IActionResult> CreateAsync(DishCreate dishCreate)
    {
        if (dishCreate.PathImage == null)
        {
            return RedirectToAction("Create");
        }
        var slip = dishCreate.PathImage.FileName.Split(".");

        if (slip != null && slip.Length >= 2)
        {
            var filename = DateTime.Now.Ticks + "." + slip[^1];
            var filePath = System.IO.Directory.GetCurrentDirectory() + "/StaticFiles/" + filename;


            using (var stream = System.IO.File.Create(filePath))
            {
                await dishCreate.PathImage.CopyToAsync(stream);
            }
            var dis = new Dish()
            {
                Name = dishCreate.Name,
                PathImage = filename,
                Status = "hidden",
                Type = dishCreate.Type,
                Price=dishCreate.Price
            };
            _context.Dish.Add(dis);
            await _context.SaveChangesAsync();

        }
        return RedirectToAction("Index");
    }
}
