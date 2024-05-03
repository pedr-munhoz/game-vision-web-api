using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace game_vision_web_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Plays",
                newName: "TargetPosition");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Plays",
                newName: "Target");

            migrationBuilder.RenameColumn(
                name: "Formation",
                table: "Plays",
                newName: "Tackler");

            migrationBuilder.AddColumn<string>(
                name: "DefensiveFormation",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefensiveNotes",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefensivePlay",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefensiveTarget",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interceptor",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfensiveNotes",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffensiveFormation",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffensivePlay",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Passer",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Penalty",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Runner",
                table: "Plays",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Safety",
                table: "Plays",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Situation",
                table: "Plays",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefensiveFormation",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "DefensiveNotes",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "DefensivePlay",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "DefensiveTarget",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Interceptor",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "OfensiveNotes",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "OffensiveFormation",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "OffensivePlay",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Passer",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Penalty",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Runner",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Safety",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Situation",
                table: "Plays");

            migrationBuilder.RenameColumn(
                name: "TargetPosition",
                table: "Plays",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Target",
                table: "Plays",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Tackler",
                table: "Plays",
                newName: "Formation");
        }
    }
}
