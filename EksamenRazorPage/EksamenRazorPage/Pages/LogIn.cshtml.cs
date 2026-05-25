using EksamenRazorPage;
using EksamenRazorPage.Models;
using Microsoft.AspNetCore.Identity;
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

        public async Task OnGetAsync()
        {

            if (!_context.Users.Any(u => u.Username == "Deafult Admin"))
            {
                var hasher = new PasswordHasher<User>();

                var defaultAdmin = new User
                {
                    Username = "Admin",
                    DisplayName = "Admin1",
                    Email = "admin@email.com",
                    IsAdmin = true
                };

                defaultAdmin.PasswordHash = hasher.HashPassword(defaultAdmin, "123");

                _context.Users.Add(defaultAdmin);

                await _context.SaveChangesAsync();
            }
        }

       

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == Username || u.Email == Username);

            if (user != null)
            {
                var hasher = new PasswordHasher<User>();

                var result = hasher.VerifyHashedPassword(user, user.PasswordHash, Password);

                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                    return RedirectToPage("/Index");
                }
            }

            Message = "Wrong information";
            return Page();
        }


    }
}