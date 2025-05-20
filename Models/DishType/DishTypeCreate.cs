using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class DishTypeCreate
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Number { get; set; }

}