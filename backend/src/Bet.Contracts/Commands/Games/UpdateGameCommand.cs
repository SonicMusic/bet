namespace Bet.Contracts.Commands.Games;

public record UpdateGameCommand(Guid GameId, Guid HomeTeamId, Guid AwayTeamId);