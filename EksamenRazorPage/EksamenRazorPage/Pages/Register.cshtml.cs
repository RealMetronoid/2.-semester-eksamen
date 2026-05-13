using EksamenRazorPage;
using EksamenRazorPage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Login.Pages
{
    public class RegisterModel : PageModel
    {

        private readonly PokemonContext _context;

        public List<User> UserList { get; set; }

        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        public string DisplayName { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string PasswordHash { get; set; }

        [BindProperty]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }



        public RegisterModel(PokemonContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            UserList = _context.Users.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. Check if user already exists
            //var existingUser = _context.Users.FirstOrDefault(p => p.Username == Username || p.Email == Email);
            //if (existingUser != null)
            //{
            //    ModelState.AddModelError("", "Username or Email already exists.");
            //    //return Page();
            //}

            // 2. Hash password
            var hasher = new PasswordHasher<User>();

            var newUser = new User
            {
                Username = Username,
                DisplayName = DisplayName,
                Email = Email
            };

            // Convert password into secure hash
            newUser.PasswordHash = hasher.HashPassword(newUser, Password);

            // 3. Save to DB
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            //4.Clear form
            ModelState.Clear();

            Username = "";
            DisplayName = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";

            ViewData["Message"] = "Registration successful!";

            return RedirectToPage("/LogIn");
        }

    }
}
