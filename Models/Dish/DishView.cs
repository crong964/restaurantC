using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class DishView
{
    public  DishCreate? DishCreate { get; set; }
    public  List<DishType>? DishTypes { set; get; }

}