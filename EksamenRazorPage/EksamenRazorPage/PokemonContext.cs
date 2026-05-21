using EksamenRazorPage.Models;
using Microsoft.EntityFrameworkCore;

namespace EksamenRazorPage
{
   public class PokemonContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public PokemonContext(DbContextOptions<PokemonContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // Max 6 pokemons per hold og må ikke duplicere slot-numre i samme hold.
            modelBuilder.Entity<TeamMember>()
             .HasIndex(tm => new { tm.TeamId, tm.Slot })
             .IsUnique();
        }
    }
}
