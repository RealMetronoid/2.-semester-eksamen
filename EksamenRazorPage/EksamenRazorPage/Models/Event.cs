namespace EksamenRazorPage.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string? Url { get; set; }
        public bool IsActive { get; set; }
        public string? EventType { get; set; }
    }
}