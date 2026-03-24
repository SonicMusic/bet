using Bet.Application.Abstractions;

namespace Bet.Application.Games.Queries;

public record GetGamesByTeamQuery(string Team, int Page, int PageSize) : IQuery;