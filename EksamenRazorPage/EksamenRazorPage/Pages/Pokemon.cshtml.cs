using EksamenRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;

namespace EksamenRazorPage.Pages
{
    public class PokemonModel : PageModel
    {
        private readonly PokemonContext _context;
        public List<Pokemon> PokemonList { get; set; } = new List<Pokemon>();

        public PokemonModel(PokemonContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            PokemonList = await _context.Pokemons.ToListAsync();
        }
    }
}
