using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class OrderDetail
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int Quality { get; set; }
    public required Dish Dish { get; set; }
}