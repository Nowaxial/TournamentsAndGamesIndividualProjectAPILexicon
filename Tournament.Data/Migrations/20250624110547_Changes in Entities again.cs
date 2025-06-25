using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tournament.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesinEntitiesagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_TournamentDetails_TournamentDetailsId",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentDetailsId",
                table: "Game",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Game",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_TournamentDetails_TournamentDetailsId",
                table: "Game",
                column: "TournamentDetailsId",
                principalTable: "TournamentDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_TournamentDetails_TournamentDetailsId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentDetailsId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_TournamentDetails_TournamentDetailsId",
                table: "Game",
                column: "TournamentDetailsId",
                principalTable: "TournamentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
