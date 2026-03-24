using System.Text;
using Bet.Application.Abstractions;
using Bet.Application.Extensions;
using Bet.Application.IoC;
using Bet.Application.Models;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games.Queries;

public class GetGamesByStatusHandler : IQueryHandler<PagedList<GameDto>, GetGamesByStatusQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ILogger<GetGamesByStatusHandler> _logger;

    public GetGamesByStatusHandler(ISqlConnectionFactory sqlConnectionFactory, ILogger<GetGamesByStatusHandler> logger)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _logger = logger;
    }

    public async Task<PagedList<GameDto>> Handle(GetGamesByStatusQuery query, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.Create();
        
        var parameters = new DynamicParameters();

        var totalCount = await connection.ExecuteScalarAsync<long>("SELECT COUNT(*) FROM games");
        
        var sql = new StringBuilder(
            """
              SELECT game_id, status FROM games
            """);
        
        if (!string.IsNullOrWhiteSpace(query.Status))
        {
            sql.Append(" WHERE status = @Status");
            parameters.Add("@Status", query.Status);
        }
        
        sql.ApplyPagination(parameters, query.Page, query.PageSize);
        
        var games = await connection.QueryAsync<GameDto>(
            sql.ToString(),
            param: parameters);
        
        return new PagedList<GameDto>
        {
            Items = games.ToList(),
            TotalCount = totalCount,
            PageSize = query.PageSize,
            Page = query.Page,
        };
    }
}