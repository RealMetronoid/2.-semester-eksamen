namespace EksamenRazorPage.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PokemonId { get; set; }

        public User User { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
