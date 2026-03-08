namespace Bet.Contracts.Commands.Games;

public record UpdateGameCommand(Guid GameId, Guid TournamentId, string HomeTeam, string AwayTeam);