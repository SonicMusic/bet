namespace Bet.Contracts.Commands.Games;

public record UpdateGameCommand(Guid GameId, string HomeTeam, string AwayTeam);