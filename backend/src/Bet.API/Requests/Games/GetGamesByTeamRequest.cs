namespace Bet.API.Requests.Games;

public record GetGamesByTeamRequest(string Team, int Page, int PageSize);