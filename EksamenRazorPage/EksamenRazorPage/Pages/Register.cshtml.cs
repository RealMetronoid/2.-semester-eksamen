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

        public RegisterModel(PokemonContext context)
        {
            _context = context;
        }

        public List<User> UserList { get; set; } = new();

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

        [BindProperty]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public User EditUser { get; set; } = new();

        [BindProperty]
        public string? NewPassword { get; set; }

        public void OnGet()
        {
            LoadUsers();
        }

        // CREATE USER / REGISTER
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadUsers();
                return Page();
            }

            var existingUsername = _context.Users.FirstOrDefault(p => p.Username == Username);

            if (existingUsername != null)
            {
                ModelState.AddModelError("", "Username already exists.");
                LoadUsers();
                return Page();
            }

            var hasher = new PasswordHasher<User>();

            var newUser = new User
            {
                Username = Username,
                DisplayName = DisplayName,
                Email = Email,
                IsAdmin = false
            };

            newUser.PasswordHash = hasher.HashPassword(newUser, Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            ModelState.Clear();

            return RedirectToPage("/LogIn");
        }

        // DELETE USER BY ID
        public async Task<IActionResult> OnPostDeleteUserByIdAsync(int id)
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        // DELETE USER BY USERNAME
        public async Task<IActionResult> OnPostDeleteUserByUsernameAsync(string username)
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        // UPDATE USER BY ID
        public async Task<IActionResult> OnPostUpdateUserByIdAsync(int id)
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.Username = EditUser.Username;
            userToUpdate.DisplayName = EditUser.DisplayName;
            userToUpdate.Email = EditUser.Email;
            userToUpdate.IsAdmin = EditUser.IsAdmin;

            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                var hasher = new PasswordHasher<User>();
                userToUpdate.PasswordHash = hasher.HashPassword(userToUpdate, NewPassword);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        // UPDATE USER BY USERNAME
        public async Task<IActionResult> OnPostUpdateUserByUsernameAsync(string oldUsername)
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Username == oldUsername);

            if (userToUpdate == null)
            {
                return NotFound();
            }

 
            userToUpdate.Username = EditUser.Username;
            userToUpdate.DisplayName = EditUser.DisplayName;
            userToUpdate.Email = EditUser.Email;
            userToUpdate.IsAdmin = EditUser.IsAdmin;

            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                var hasher = new PasswordHasher<User>();
                userToUpdate.PasswordHash = hasher.HashPassword(userToUpdate, NewPassword);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }


        private void LoadUsers()
        {
            UserList = _context.Users.ToList();
        }


        private bool CurrentUserIsAdmin()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return false;
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId.Value);

            if (user == null)
            {
                return false;
            }

            return user.IsAdmin;
        }
    }
}