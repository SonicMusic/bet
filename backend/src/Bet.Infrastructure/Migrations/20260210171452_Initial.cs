using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "team",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    number_goals_scored = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_team", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    home_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    away_team_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_games", x => x.id);
                    table.ForeignKey(
                        name: "fk_games_team_away_team_id",
                        column: x => x.away_team_id,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_games_team_home_team_id",
                        column: x => x.home_team_id,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_games_away_team_id",
                table: "games",
                column: "away_team_id");

            migrationBuilder.CreateIndex(
                name: "ix_games_home_team_id",
                table: "games",
                column: "home_team_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "team");
        }
    }
}
