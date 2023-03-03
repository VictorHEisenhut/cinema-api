using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPINET6.Migrations
{
    /// <inheritdoc />
    public partial class Sessaoecinema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaID",
                table: "Sessoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_CinemaID",
                table: "Sessoes",
                column: "CinemaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Cinemas_CinemaID",
                table: "Sessoes",
                column: "CinemaID",
                principalTable: "Cinemas",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Cinemas_CinemaID",
                table: "Sessoes");

            migrationBuilder.DropIndex(
                name: "IX_Sessoes_CinemaID",
                table: "Sessoes");

            migrationBuilder.DropColumn(
                name: "CinemaID",
                table: "Sessoes");
        }
    }
}
