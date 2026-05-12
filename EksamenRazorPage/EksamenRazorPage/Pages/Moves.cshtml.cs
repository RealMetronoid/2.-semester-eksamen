using EksamenRazorPage;
using EksamenRazorPage.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EksamenRazorPage.Pages
{
    public class MovesModel : PageModel
    {
        private readonly PokemonContext _context;
        public List<Move> MoveList { get; set; } = new();

        public MovesModel(PokemonContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            MoveList = await _context.Moves.ToListAsync();
        }
    }
}