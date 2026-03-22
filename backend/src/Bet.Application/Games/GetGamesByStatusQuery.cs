using Bet.Application.Abstractions;

namespace Bet.Application.Games;

public record GetGamesByStatusQuery(string Status, int Page, int PageSize) : IQuery;