using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace game_vision_web_api.Migrations
{
    /// <inheritdoc />
    public partial class AdjustRelationGameWithPlays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Games_GameId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GameId",
                table: "Games",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameId",
                table: "Games",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Games_GameId",
                table: "Games",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
