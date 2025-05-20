using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _netmvc.Models;

namespace _netmvc.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<_netmvc.Models.Movie> Movie { get; set; } = default!;
        public DbSet<_netmvc.Models.DishType> DishType { get; set; } = default!;
        public DbSet<_netmvc.Models.Table> Table { get; set; } = default!;
        public DbSet<_netmvc.Models.Dish> Dish { get; set; } = default!;
        public DbSet<_netmvc.Models.OrderDetail> OrderDetail { get; set; } = default!;
    }
}
