namespace EksamenRazorPage.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
        public List<TeamMember> Members { get; set; } = new();
    }

    public class TeamMember
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int PokemonId { get; set; }
        public int Slot { get; set; } // 1–6

        public Team Team { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
