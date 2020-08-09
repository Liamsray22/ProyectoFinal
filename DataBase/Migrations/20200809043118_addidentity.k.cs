using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class addidentityk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Votacion__IdPart__1DE57479",
                table: "Votacion");

            migrationBuilder.RenameColumn(
                name: "IdPartido",
                table: "Votacion",
                newName: "IdCandidato");

            migrationBuilder.RenameIndex(
                name: "IX_Votacion_IdPartido",
                table: "Votacion",
                newName: "IX_Votacion_IdCandidato");

            migrationBuilder.AddForeignKey(
                name: "FK_Votacion_IdCand_1EE45789",
                table: "Votacion",
                column: "IdCandidato",
                principalTable: "Candidatos",
                principalColumn: "IdCandidato",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votacion_IdCand_1EE45789",
                table: "Votacion");

            migrationBuilder.RenameColumn(
                name: "IdCandidato",
                table: "Votacion",
                newName: "IdPartido");

            migrationBuilder.RenameIndex(
                name: "IX_Votacion_IdCandidato",
                table: "Votacion",
                newName: "IX_Votacion_IdPartido");

            migrationBuilder.AddForeignKey(
                name: "FK__Votacion__IdPart__1DE57479",
                table: "Votacion",
                column: "IdPartido",
                principalTable: "Partidos",
                principalColumn: "IdPartido",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
