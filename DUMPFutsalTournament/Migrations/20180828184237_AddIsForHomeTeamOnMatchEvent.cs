using Microsoft.EntityFrameworkCore.Migrations;

namespace DUMPFutsalTournament.Migrations
{
    public partial class AddIsForHomeTeamOnMatchEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForHomeTeam",
                table: "MatchEvents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForHomeTeam",
                table: "MatchEvents");
        }
    }
}
