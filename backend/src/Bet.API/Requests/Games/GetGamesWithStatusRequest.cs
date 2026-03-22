using Bet.Application.Games;

namespace Bet.API.Requests.Games;

public record GetGamesWithStatusRequest(string Status, int Page, int PageSize);