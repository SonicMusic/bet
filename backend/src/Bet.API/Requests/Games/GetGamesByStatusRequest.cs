namespace Bet.API.Requests.Games;

public record GetGamesByStatusRequest(string Status, int Page, int PageSize);