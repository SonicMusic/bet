namespace Bet.Contracts.Requests.Games;

public record CreateGameRequest(Guid HomeTeamId, Guid AwayTeamId);