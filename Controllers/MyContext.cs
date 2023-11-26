// Control de errores
#pragma warning disable CS8618

// Instanciar EntityFramework
using Microsoft.EntityFrameworkCore;

namespace CrudDelicious.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<Dish> Dishes { get; set; }

    }
}