using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class DishTypeView
{
    public int Id { get; set; }
    [MinLength(3)]
    public required string Name { get; set; }
    public int number { get; set; }

}