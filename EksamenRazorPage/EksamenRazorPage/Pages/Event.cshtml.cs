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

        public List<Event> AllEvents { get; set; } = new();


        public bool IsAdmin { get; set; }

        [BindProperty]
        public Event NewEvent { get; set; } = new();

        [BindProperty]
        public Event EditEvent { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SelectedEventType { get; set; }

        public void OnGet()
        {
            IsAdmin = CurrentUserIsAdmin();
            LoadEvents();
        }

        // CREATE EVENT
        public IActionResult OnPostAddEvent()
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            if (string.IsNullOrWhiteSpace(NewEvent.Name) || string.IsNullOrWhiteSpace(NewEvent.Description))
            {
                IsAdmin = CurrentUserIsAdmin();
                LoadEvents();
                ModelState.AddModelError("", "Name and description are required.");
                return Page();
            }

            NewEvent.Id = 0;
            NewEvent.IsActive = true;

            _context.Events.Add(NewEvent);
            _context.SaveChanges();

            return RedirectToPage("/Event");
        }

        // UPDATE EVENT
        public IActionResult OnPostUpdateEvent()
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            if (EditEvent.Id <= 0)
            {
                LoadEvents();
                IsAdmin = CurrentUserIsAdmin();
                ModelState.AddModelError("", "No event was selected.");
                return Page();
            }

            if (string.IsNullOrWhiteSpace(EditEvent.Name) || string.IsNullOrWhiteSpace(EditEvent.Description))
            {
                LoadEvents();
                IsAdmin = CurrentUserIsAdmin();
                ModelState.AddModelError("", "Name and description are required.");
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

            // Keep it active after update
            existingEvent.IsActive = true;

            _context.SaveChanges();

            return RedirectToPage("/Event");
        }

        // DELETE EVENT
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

            _context.Events.Remove(eventToDelete);
            _context.SaveChanges();

            return RedirectToPage();
        }

        // InActive EVENT
        public IActionResult OnPostInactiveEvent(int id)
        {
            if (!CurrentUserIsAdmin())
            {
                return RedirectToPage("/Index");
            }

            var eventToInactive = _context.Events.FirstOrDefault(e => e.Id == id);

            if (eventToInactive == null)
            {
                return NotFound();
            }

            eventToInactive.IsActive = false;

            _context.SaveChanges();

            return RedirectToPage();
        }

        private void LoadEvents()
        {
            var query = _context.Events
                .Where(e => e.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SelectedEventType) && SelectedEventType != "Alle")
            {
                query = query.Where(e => e.EventType == SelectedEventType);
            }

            Events = query.OrderBy(e => e.Date).ToList();

            AllEvents = _context.Events.OrderBy(e => e.Date).ToList();
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