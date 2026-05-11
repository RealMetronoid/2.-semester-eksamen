using EksamenRazorPage;
using EksamenRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace Login.Pages
{
    public class LogInModel : PageModel
    {
        private readonly PokemonContext _context;

        public List<User> UserList { get; set; }

        public LogInModel(PokemonContext context)
        {
            _context = context;
        }
        bool success;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]

        public string Message { get; set; }

        public void OnGet()
        {
            UserList = _context.Users.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            foreach (User person in UserList)
            {
                if (Username == person.Username || Username == person.Email && Password == person.PasswordHash)
                {
                    Message = "Welcome";
                    success = true;
                }
                else
                {
                    Message = "Wrong information";
                }

            }

            if (success == true)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostLogInAsync()
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    (u.Username == Username || u.Email == Username)
                    && u.PasswordHash == Password);

            if (user == null)
            {
                Message = "Forkert brugernavn eller adgangskode";
                return Page();
            }

           
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToPage("/Index");
        }

    }
}
