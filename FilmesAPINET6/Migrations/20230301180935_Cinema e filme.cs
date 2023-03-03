using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPINET6.Migrations
{
    /// <inheritdoc />
    public partial class Cinemaefilme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Cinemas_CinemaID",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_FilmeID",
                table: "Sessoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessoes",
                table: "Sessoes");

            migrationBuilder.DropIndex(
                name: "IX_Sessoes_FilmeID",
                table: "Sessoes");

            migrationBuilder.DropColumn(
                name: "ID",
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

            migrationBuilder.AlterColumn<int>(
                name: "CinemaID",
                table: "Sessoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessoes",
                table: "Sessoes",
                columns: new[] { "FilmeID", "CinemaID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Cinemas_CinemaID",
                table: "Sessoes",
                column: "CinemaID",
                principalTable: "Cinemas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_FilmeID",
                table: "Sessoes",
                column: "FilmeID",
                principalTable: "Filmes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Cinemas_CinemaID",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_FilmeID",
                table: "Sessoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessoes",
                table: "Sessoes");

            migrationBuilder.AlterColumn<int>(
                name: "CinemaID",
                table: "Sessoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FilmeID",
                table: "Sessoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Sessoes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessoes",
                table: "Sessoes",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_FilmeID",
                table: "Sessoes",
                column: "FilmeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Cinemas_CinemaID",
                table: "Sessoes",
                column: "CinemaID",
                principalTable: "Cinemas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_FilmeID",
                table: "Sessoes",
                column: "FilmeID",
                principalTable: "Filmes",
                principalColumn: "ID");
        }
    }
}
