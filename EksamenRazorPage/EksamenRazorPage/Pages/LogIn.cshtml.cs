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
            UserList = _context.Users.ToList();
        }

       

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == Username ||
                u.Email == Username);

            if (user != null)
            {
                var hasher = new PasswordHasher<User>();

                var result = hasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    Password);

                if (result == PasswordVerificationResult.Success)
                {
                    return RedirectToPage("/Index");
                }
            }

            Message = "Wrong information";
            return Page();
        }


    }
}