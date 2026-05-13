using EksamenRazorPage;
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
        public bool IsLoggedIn { get; set; }

        public TeamModel(PokemonContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var username = HttpContext.Session.GetString("Username");
            IsLoggedIn = username != null;

            if (IsLoggedIn)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username);

                Teams = await _context.Teams
                    .Where(t => t.UserId == user.Id)
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync(string teamName)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null) return RedirectToPage("/LogIn");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            _context.Teams.Add(new Team
            {
                Name = teamName,
                UserId = user.Id
            });
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}