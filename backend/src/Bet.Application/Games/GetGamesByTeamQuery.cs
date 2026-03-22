using Bet.Application.Abstractions;

namespace Bet.Application.Games;

public record GetGamesByTeamQuery(string Team, int Page, int PageSize) : IQuery;