using EksamenRazorPage.Models;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void AddMove_ShouldAddMoveToMovepool()
        {
            // Arrange - Create a Pokemon and a Move
            var pokemon = new Pokemon(1, "Pikachu", "Electric",
                35, 55, 40, 50, 50, 90, "pikachu.png");

            var move = new Move
            {
                Id = 1,
                Name = "Thunderbolt",
                Type = "Electric",
                Power = 90,
                PowerPoints = 15,
                Category = "Special",
                Effect = "May paralyze the target."
            };

            // Act - Call AddMove
            pokemon.AddMove(move);

            // Assert - Movepool should now contain the move
            Assert.Single(pokemon.Movepool);
            Assert.Contains(move, pokemon.Movepool);
        }
    }
}


