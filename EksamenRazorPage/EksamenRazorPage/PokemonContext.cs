using EksamenRazorPage.Models;
using Microsoft.EntityFrameworkCore;

namespace EksamenRazorPage
{
   public class PokemonContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Move> Moves { get; set; }

        public PokemonContext(DbContextOptions<PokemonContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
