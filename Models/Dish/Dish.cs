using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class Dish
{
    public int Id { get; set; }
    [MinLength(3)]
    public required string Name { get; set; }

    [MinLength(3)]
    public required string Type { get; set; }
    public required string PathImage { get; set; }
    public int Price { get; set; }

    [DefaultValue("hidden")]
    public required string Status { get; set; }

}