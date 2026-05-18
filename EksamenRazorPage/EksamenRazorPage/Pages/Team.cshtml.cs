using EksamenRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EksamenRazorPage.Pages
{
    public class TeamModel : PageModel
    {
        private readonly PokemonContext _context;
        public List<Team> Teams { get; set; } = new();
        public List<Pokemon> AllPokemon { get; set; } = new();

        public TeamModel(PokemonContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null) return RedirectToPage("/LogIn");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            Teams = await _context.Teams
                .Where(t => t.UserId == user.Id)
                .Include(t => t.Members)
                .ToListAsync();

            AllPokemon = await _context.Pokemons.OrderBy(p => p.Id).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync(string teamName)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null) return RedirectToPage("/LogIn");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            _context.Teams.Add(new Team { Name = teamName, UserId = user.Id });
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddMemberAsync(int teamId, int pokemonId)
        {
            var team = await _context.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null || team.Members.Count >= 6 || pokemonId == 0)
                return RedirectToPage();

            var usedSlots = team.Members.Select(m => m.Slot).ToHashSet();
            var freeSlot = Enumerable.Range(1, 6).First(s => !usedSlots.Contains(s));

            _context.TeamMembers.Add(new TeamMember
            {
                TeamId = teamId,
                PokemonId = pokemonId,
                Slot = freeSlot
            });
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveMemberAsync(int memberId)
        {
            var member = await _context.TeamMembers.FindAsync(memberId);
            if (member != null)
            {
                _context.TeamMembers.Remove(member);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}