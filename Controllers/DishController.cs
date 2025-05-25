using System.Threading.Tasks;
using _netmvc.Data;
using _netmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Protocol;

namespace _netmvc.Controllers;

[RequestSizeLimit(100_000_000_000_000)]
public class DishController : Controller
{
    [BindProperty]
    public required DishCreate DishCreate { get; set; }
    private readonly MvcMovieContext _context;
    public DishController(MvcMovieContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index(int? idtype)
    {
        ViewData["ima"] = "https://localhost:7019/sta/638833544375746922.jpg";
        ViewData["lstype"] = await _context.DishType.ToArrayAsync();
        ViewData["idtype"] = idtype;
        if (idtype == null)
        {
            return View(await _context.Dish.ToListAsync());
        }
        return View(await _context.Dish.Where(i => i.Type.Equals(idtype+"")).ToArrayAsync());
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
                Price = dishCreate.Price
            };
            _context.Dish.Add(dis);
            await _context.SaveChangesAsync();

        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }

        var dish = await _context.Dish.FindAsync(id);
        if (dish == null)
        {
            return RedirectToAction("Index");
        }

        ViewData["oldimg"] = dish.PathImage;
        return View(new DishEditView
        {
            DishTypes = await _context.DishType.ToListAsync(),
            DishEdit = new DishEdit
            {
                Name = dish.Name,
                Price = dish.Price,
                Type = dish.Type,
                Id = dish.Id,
                PathImage = null,
                Status = ""
            }
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DishEdit? dishEdit)
    {
        if (dishEdit == null)
        {

            return RedirectToAction("Index");
        }
        var olddis = await _context.Dish.FindAsync(dishEdit.Id);

        if (olddis == null)
        {

            return RedirectToAction("Index");
        }
        olddis.Name = dishEdit.Name;
        olddis.Status = "hidden";
        olddis.Type = dishEdit.Type;
        olddis.Price = dishEdit.Price;

        if (dishEdit.PathImage != null)
        {
            var slip = dishEdit.PathImage.FileName.Split(".");

            if (slip != null && slip.Length >= 2)
            {
                var filename = DateTime.Now.Ticks + "." + slip[^1];
                var filePath = System.IO.Directory.GetCurrentDirectory() + "/StaticFiles/" + filename;
                using (var stream = System.IO.File.Create(filePath))
                {
                    await dishEdit.PathImage.CopyToAsync(stream);
                }
                var oldfile = System.IO.Directory.GetCurrentDirectory() + "/StaticFiles/" + olddis.PathImage;
                if (System.IO.File.Exists(oldfile))
                {
                    System.IO.File.Delete(oldfile);
                }
                olddis.PathImage = filename;
            }

        }


        _context.Dish.Update(olddis);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public IActionResult Details(int? id)
    {

        return View();
    }
}
