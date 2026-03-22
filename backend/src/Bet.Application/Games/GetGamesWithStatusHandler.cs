using Bet.Application.IoC;
using Bet.Application.Models;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games;

public class GetAllGamesHandler
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ILogger<GetAllGamesHandler> _logger;

    public GetAllGamesHandler(ISqlConnectionFactory sqlConnectionFactory, ILogger<GetAllGamesHandler> logger)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _logger = logger;
    }

    public async Task<PagedList<GameDto>> Handle()
    {
        
    }
}