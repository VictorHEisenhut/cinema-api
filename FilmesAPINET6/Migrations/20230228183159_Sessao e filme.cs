using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPINET6.Migrations
{
    /// <inheritdoc />
    public partial class Sessaoefilme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessoes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessoes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sessoes_Filmes_FilmeID",
                        column: x => x.FilmeID,
                        principalTable: "Filmes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_FilmeID",
                table: "Sessoes",
                column: "FilmeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessoes");
        }
    }
}
