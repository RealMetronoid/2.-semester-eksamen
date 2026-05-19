using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EksamenRazorPage.Migrations
{
    /// <inheritdoc />
    public partial class FixMove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Pokemons_PokemonId",
                table: "Moves");

            migrationBuilder.DropIndex(
                name: "IX_Moves_PokemonId",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "Moves");

            migrationBuilder.CreateTable(
                name: "MovePokemon",
                columns: table => new
                {
                    MovepoolId = table.Column<int>(type: "int", nullable: false),
                    PokemonCanLearnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovePokemon", x => new { x.MovepoolId, x.PokemonCanLearnId });
                    table.ForeignKey(
                        name: "FK_MovePokemon_Moves_MovepoolId",
                        column: x => x.MovepoolId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovePokemon_Pokemons_PokemonCanLearnId",
                        column: x => x.PokemonCanLearnId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovePokemon_PokemonCanLearnId",
                table: "MovePokemon",
                column: "PokemonCanLearnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovePokemon");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "Moves",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moves_PokemonId",
                table: "Moves",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Pokemons_PokemonId",
                table: "Moves",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id");
        }
    }
}
