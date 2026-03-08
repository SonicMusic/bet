namespace Bet.Contracts.Requests.Games;

public record UpdateGameRequest(Guid TournamentId, string HomeTeam, string AwayTeam);