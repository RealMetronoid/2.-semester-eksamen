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


        [BindProperty(SupportsGet = true)]
        public string? SelectedEventType { get; set; }

        public void OnGet()
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


        public IActionResult OnPostAddEvent()
        {
            if (!ModelState.IsValid)
            {
                Events = _context.Events
                    .OrderBy(e => e.Date)
                    .ToList();

                return Page();
            }

            _context.Events.Add(NewEvent);
            _context.SaveChanges();

            return RedirectToPage();
        }

    }
}