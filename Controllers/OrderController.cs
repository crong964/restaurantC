using System.Threading.Tasks;
using _netmvc.Data;
using _netmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _netmvc.Controllers;

public class OrderController : Controller
{
    private readonly MvcMovieContext _context;
    public OrderController(MvcMovieContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {

        return View(await _context.Table.ToListAsync());
    }
    public async Task<IActionResult> Order(int? idtable)
    {

        if (idtable == null)
        {
            return RedirectToAction("Index");
        }
        var Table = _context.Table.Where((i => i.Id == idtable)).First();
        if (Table == null)
        {
            return RedirectToAction("Index");
        }
        var orderdetail = await _context.OrderDetail.Where((i => i.TableId == idtable)).ToListAsync();
        var dishes = _context.Dish.ToList();

        var OrderDetailView = new OrderDetailView
        {
            Dishes = dishes,
            OrderDetails = orderdetail,
            Table = Table
        };
        ViewData["money"] = orderdetail.Where(i => i.TableId == idtable).Sum(i => i.Dish.Price * i.Quality); ;



        return View(OrderDetailView);
    }

    [HttpPost]
    public async Task<IActionResult> AddDishAsync(int? idtable, int? iddish)
    {
        if (idtable == null || iddish == null)
        {
            return RedirectToAction("Index");
        }
        var orderdetailsingle = _context.OrderDetail.Where(i => i.TableId == idtable && i.Dish.Id == iddish).FirstOrDefault();
        var dish = _context.Dish.Single(i => i.Id == iddish);

        var table = _context.Table.Single(i => i.Id == idtable);

        table.status = "order";
        _context.Table.Update(table);
        await _context.SaveChangesAsync();
        if (orderdetailsingle == null)
        {
            var orderdetail = new Models.OrderDetail
            {
                Quality = 1,
                TableId = (int)idtable,
                Dish = dish,
            };
            _context.OrderDetail.Add(orderdetail);
            await _context.SaveChangesAsync();
        }
        else
        {
            orderdetailsingle.Quality += 1;
            _context.OrderDetail.Update(orderdetailsingle);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Order", new { idtable = idtable });
    }
    public async Task<IActionResult> DeleteDish(int? idtable, int? iddish)
    {
        if (idtable == null || iddish == null)
        {
            return RedirectToAction("Index");
        }
        var orderdetailsingle = _context.OrderDetail.Where(i => i.TableId == idtable && i.Dish.Id == iddish).FirstOrDefault();
        var dish = _context.Dish.Single(i => i.Id == iddish);

        var table = _context.Table.Single(i => i.Id == idtable);

        table.status = "order";
        _context.Table.Update(table);
        await _context.SaveChangesAsync();
        if (orderdetailsingle == null)
        {

        }
        else
        {
            orderdetailsingle.Quality -= 1;
            if (orderdetailsingle.Quality == 0)
            {
                _context.OrderDetail.Remove(orderdetailsingle);
            }
            else
            {
                _context.OrderDetail.Update(orderdetailsingle);
            }

            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Order", new { idtable = idtable });
    }

    public void Serve()
    {

    }
}