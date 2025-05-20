using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class OrderDetailCreate
{
    public int Id { get; set; }
    public int DishId { get; set; }
    public int TableId { get; set; }
    public int Quality { get; set; }

}