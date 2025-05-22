using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class DishEdit
{
    public int Id { get; set; }
    [MinLength(3)]
    public required string Name { get; set; }

   
    public required string Type { get; set; }
    public IFormFile? PathImage { get; set; }
    public int Price { get; set; }

    [DefaultValue("hidden")]
    public required string Status { get; set; }

}