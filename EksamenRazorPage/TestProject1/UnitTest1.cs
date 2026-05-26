using EksamenRazorPage;
using Login.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public async Task PasswordNotMatching_ReturnsPageResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PokemonContext>()
                .UseInMemoryDatabase(databaseName: "RegisterTestDb")
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
            model.ModelState.AddModelError("ConfirmPassword", "Passwords do not match");

            // Act
            var result = await model.OnPostCreateUser();

            // Assert
            Assert.IsType<PageResult>(result);
        }




    }
}
