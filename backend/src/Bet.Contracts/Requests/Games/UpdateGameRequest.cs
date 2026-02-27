namespace Bet.Contracts.Requests.Games;

public record UpdateGameRequest(Guid HomeTeamId, Guid AwayTeamId);