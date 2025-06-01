using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace _netmvc.Models;

public class OrderDetail
{
    public int Id { get; set; }
   
    public int Quality { get; set; }

    [DefaultValue(0)]
    public int Served { get; set; }
    public required Dish Dish { get; set; }
    public required Table Table { get; set; }

    public string CreateTime { get; set; } = DateTime.Now.Ticks.ToString();
}