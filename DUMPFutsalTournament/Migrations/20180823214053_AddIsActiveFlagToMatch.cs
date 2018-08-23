using Microsoft.EntityFrameworkCore.Migrations;

namespace DUMPFutsalTournament.Migrations
{
    public partial class AddIsActiveFlagToMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Matches",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Matches");
        }
    }
}
