using System.ComponentModel.DataAnnotations;

namespace _netmvc.Models;

public class Login
{
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public required string acc { get; set; }
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public required string pw { get; set; }
}