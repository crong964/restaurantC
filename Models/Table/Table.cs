using System.ComponentModel;

namespace _netmvc.Models;

public class Table
{
    public int Id { get; set; }
    public int number { get; set; }

    [DefaultValue("empty")]
   
    public required string status { get; set; }
}