using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EksamenRazorPage;
using EksamenRazorPage.Models;
using Microsoft.EntityFrameworkCore;

namespace EksamenRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PokemonContext _context;

        public List<Pokemon> FeaturedPokemon { get; set; } = new();

        public IndexModel(PokemonContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            FeaturedPokemon = await _context.Pokemons.Take(6).ToListAsync();
        }
    }
}
