namespace Bet.Contracts.Commands.Games;

public record CreateGameCommand(Guid TournamentId, string HomeTeam, string AwayTeam);