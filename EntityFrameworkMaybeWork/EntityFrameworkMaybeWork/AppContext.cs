using EntityFrameworkMaybeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkMaybeWork
{
    public class AppContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            // You don't actually ever need to call this. Entityframework is just retarded.
        }

    }
}
