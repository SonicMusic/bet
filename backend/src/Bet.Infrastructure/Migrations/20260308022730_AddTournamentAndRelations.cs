using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTournamentAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    short_name = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    logo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tournaments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    logo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tournaments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tournament_id = table.Column<Guid>(type: "uuid", nullable: false),
                    home_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    away_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    home_team_goals = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    away_team_goals = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "NotStarted"),
                    start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_games", x => x.id);
                    table.ForeignKey(
                        name: "fk_games_teams_away_team_id",
                        column: x => x.away_team_id,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_games_teams_home_team_id",
                        column: x => x.home_team_id,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_games_tournaments_tournament_id",
                        column: x => x.tournament_id,
                        principalTable: "tournaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tournament_teams",
                columns: table => new
                {
                    tournament_id = table.Column<Guid>(type: "uuid", nullable: false),
                    team_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tournament_teams", x => new { x.tournament_id, x.team_id });
                    table.ForeignKey(
                        name: "fk_tournament_teams_teams_team_id",
                        column: x => x.team_id,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tournament_teams_tournaments_tournament_id",
                        column: x => x.tournament_id,
                        principalTable: "tournaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "predictions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    game_id = table.Column<Guid>(type: "uuid", nullable: false),
                    home_team_goals = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    away_team_goals = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Pending"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_predictions", x => x.id);
                    table.ForeignKey(
                        name: "fk_predictions_games_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
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

            migrationBuilder.CreateIndex(
                name: "ix_games_tournament_id",
                table: "games",
                column: "tournament_id");

            migrationBuilder.CreateIndex(
                name: "ix_predictions_game_id",
                table: "predictions",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_teams_name",
                table: "teams",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tournament_teams_team_id",
                table: "tournament_teams",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "ix_tournaments_name",
                table: "tournaments",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "predictions");

            migrationBuilder.DropTable(
                name: "tournament_teams");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "tournaments");
        }
    }
}
