namespace Bet.Contracts.Requests.Games;

public record CreateGameRequest(Guid TournamentId, string HomeTeam, string AwayTeam);