using Bet.Application.Abstractions;

namespace Bet.Application.Games;

public record GetGamesWithStatusQuery(string Status, int Page, int PageSize) : IQuery;