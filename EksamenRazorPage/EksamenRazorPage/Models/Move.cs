namespace EksamenRazorPage.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PowerPoints { get; set; }
        public int Power { get; set; }
        public string Category { get; set; }
        public string Effect { get; set; }
        public string? SecondaryEffect { get; set; }

        public Move(int id, string name, string type, int powerPoints, int power, string category, string effect, string? secondaryEffect)
        {
            Id = id;
            Name = name;
            Type = type;
            PowerPoints = powerPoints;
            Power = power;
            Category = category;
            Effect = effect;
            SecondaryEffect = secondaryEffect;
        }
    }
}
