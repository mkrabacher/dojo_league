using dojo_league.Models;
using Microsoft.EntityFrameworkCore;
 
namespace dojo_league.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Ninja> Ninjas { get; set; }
        public DbSet<Dojo> Dojos { get; set; }
    }
}
