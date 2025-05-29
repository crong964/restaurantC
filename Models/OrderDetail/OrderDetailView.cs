using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class OrderDetailView
{
    public required Table Table { set; get; }
    public List<OrderDetail>? OrderDetails { set; get; }
    public required List<Dish> Dishes { set; get; }
}