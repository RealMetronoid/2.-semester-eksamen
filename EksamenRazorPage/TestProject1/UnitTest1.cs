using EksamenRazorPage;
using EksamenRazorPage.Models;
using Login.Pages;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Xunit;

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

        [Fact]
        public async Task PasswordNotMatching_ReturnsPageResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PokemonContext>()
                .UseInMemoryDatabase("RegisterTestDb")
                .Options;

            var context = new PokemonContext(options);

            var model = new RegisterModel(context)
            {
                Username = "Marcus",
                DisplayName = "Marc3935",
                Email = "a@gmail.com",
                Password = "abc",
                ConfirmPassword = "xyz"
            };

            // Simulate invalid ModelState
            model.ModelState.AddModelError(
                "ConfirmPassword",
                "Passwords do not match");

            // Act
            var result = await model.OnPostCreateUser();

            // Assert
            Assert.IsType<PageResult>(result);
        }




    }
}


