using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EksamenRazorPage.Migrations
{
    /// <inheritdoc />
    public partial class PokemonClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HP",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defense",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpAtk",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpDef",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Defense",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "HP",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "SpAtk",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "SpDef",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Pokemons");
        }
    }
}
