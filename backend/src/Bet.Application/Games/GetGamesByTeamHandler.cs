using Bet.Application.Abstractions;
using Bet.Application.Extensions;
using Bet.Application.Models;
using Bet.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games;

public class GetGamesByTeamHandler : IQueryHandler<PagedList<GameDto>, GetGamesByTeamQuery>
{
    private readonly IReadDbContext _context;
    private readonly ILogger<GetGamesByTeamHandler> _logger;

    public GetGamesByTeamHandler(IReadDbContext context, ILogger<GetGamesByTeamHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PagedList<GameDto>> Handle(GetGamesByTeamQuery query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query.Team))
        {
            return new PagedList<GameDto>
            {
                Items = [],
                TotalCount = 0,
                PageSize = query.PageSize,
                Page = query.Page,
            };
        }

        var teamName = query.Team.Trim();

        var gamesQuery =
            from g in _context.Games
            join home in _context.Teams on g.HomeTeamId equals home.Id
            join away in _context.Teams on g.AwayTeamId equals away.Id
            where home.Name == teamName || away.Name == teamName
            select new
            {
                g.GameId,
                g.HomeTeamId,
                g.AwayTeamId,
                g.HomeTeamGoals,
                g.AwayTeamGoals,
                g.Status,
                g.Start,
            };

        var paged = await gamesQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);

        return new PagedList<GameDto>
        {
            Items = paged.Items
                .Select(x => new GameDto
                {
                    GameId = x.GameId,
                    HomeTeamId = x.HomeTeamId,
                    AwayTeamId = x.AwayTeamId,
                    HomeTeamGoals = x.HomeTeamGoals,
                    AwayTeamGoals = x.AwayTeamGoals,
                    Status = x.Status.ToString(),
                    Start = x.Start,
                })
                .ToList(),
            TotalCount = paged.TotalCount,
            PageSize = paged.PageSize,
            Page = paged.Page,
        };
    }
    
    
}