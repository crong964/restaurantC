using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class DishEditView
{
    public DishEdit? DishEdit { get; set; }
    public List<DishType>? DishTypes { set; get; }

}