using EksamenRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EksamenRazorPage.Pages
{
    public class EventModel : PageModel
    {
        private readonly PokemonContext _context;

        public EventModel(PokemonContext context)
        {
            _context = context;
        }

        public List<Event> Events { get; set; } = new();

        [BindProperty]
        public Event NewEvent { get; set; } = new();

        [BindProperty]
        public Event EditEvent { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SelectedEventType { get; set; }

        public void OnGet()
        {
            LoadEvents();
        }

        // CREATE EVENT - ADMIN ONLY
        public IActionResult OnPostAddEvent()
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            if (!ModelState.IsValid)
            {
                LoadEvents();
                return Page();
            }

            NewEvent.IsActive = true;

            _context.Events.Add(NewEvent);
            _context.SaveChanges();

            return RedirectToPage();
        }

        // UPDATE EVENT - ADMIN ONLY
        public IActionResult OnPostUpdateEvent()
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            if (!ModelState.IsValid)
            {
                LoadEvents();
                return Page();
            }

            var existingEvent = _context.Events.FirstOrDefault(e => e.Id == EditEvent.Id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            existingEvent.Name = EditEvent.Name;
            existingEvent.Description = EditEvent.Description;
            existingEvent.Date = EditEvent.Date;
            existingEvent.Url = EditEvent.Url;
            existingEvent.EventType = EditEvent.EventType;
            existingEvent.IsActive = EditEvent.IsActive;

            _context.SaveChanges();

            return RedirectToPage();
        }

        // DELETE EVENT - ADMIN ONLY
        public IActionResult OnPostDeleteEvent(int id)
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            var eventToDelete = _context.Events.FirstOrDefault(e => e.Id == id);

            if (eventToDelete == null)
            {
                return NotFound();
            }

            // Soft delete recommended because you already use IsActive
            eventToDelete.IsActive = false;

            // If you want permanent delete instead, use this:
            // _context.Events.Remove(eventToDelete);

            _context.SaveChanges();

            return RedirectToPage();
        }

        private void LoadEvents()
        {
            var query = _context.Events
                .Where(e => e.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SelectedEventType) &&
                SelectedEventType != "Alle")
            {
                query = query.Where(e => e.EventType == SelectedEventType);
            }

            Events = query
                .OrderBy(e => e.Date)
                .ToList();
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