using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPINET6.Migrations
{
    /// <inheritdoc />
    public partial class FilmeIDnulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_FilmeID",
                table: "Sessoes");

            migrationBuilder.AlterColumn<int>(
                name: "FilmeID",
                table: "Sessoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_FilmeID",
                table: "Sessoes",
                column: "FilmeID",
                principalTable: "Filmes",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_FilmeID",
                table: "Sessoes");

            migrationBuilder.AlterColumn<int>(
                name: "FilmeID",
                table: "Sessoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_FilmeID",
                table: "Sessoes",
                column: "FilmeID",
                principalTable: "Filmes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
