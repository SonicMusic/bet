namespace Bet.Contracts.Commands.Games;

public record CreateGameCommand(Guid HomeTeamId, Guid AwayTeamId);