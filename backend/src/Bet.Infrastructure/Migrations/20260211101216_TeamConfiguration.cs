using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TeamConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_team",
                table: "team");

            migrationBuilder.RenameTable(
                name: "team",
                newName: "teams");

            migrationBuilder.AddPrimaryKey(
                name: "pk_teams",
                table: "teams",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_teams",
                table: "teams");

            migrationBuilder.RenameTable(
                name: "teams",
                newName: "team");

            migrationBuilder.AddPrimaryKey(
                name: "pk_team",
                table: "team",
                column: "id");
        }
    }
}
