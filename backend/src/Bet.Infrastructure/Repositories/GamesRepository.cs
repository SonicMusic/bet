using Bet.Domain.GameManagement;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Bet.Infrastructure.Repositories;

public class GamesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GamesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Game game, CancellationToken cancellationToken)
    {
        await _dbContext.Games.AddAsync(game, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return game.Id;
    }

    public async Task<Result<Game, string>> GetById(Guid guid)
    {
        var game = await _dbContext.Games
            .Include(g => g.HomeTeam)
            .Include(g => g.AwayTeam)
            .FirstOrDefaultAsync(g => g.Id == guid);

        if (game is null)
            return "Game not found";

        return game;
    }
}