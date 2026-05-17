using EksamenRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace EksamenRazorPage.Pages
{
    public class ViewModel : PageModel
    {
        private readonly PokemonContext _context;

        public Pokemon SelectedPokemon;

        public ViewModel(PokemonContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync(int id)
        {
            //  Find the pokemon we clicked on in the list and get it from the database.
            SelectedPokemon = await _context.Pokemons.FindAsync(id);
        }
    }
}
