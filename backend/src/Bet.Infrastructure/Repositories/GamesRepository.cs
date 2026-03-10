using Bet.Application.IoC;
using Bet.Domain.GameManagement;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Bet.Infrastructure.Repositories;

public class GamesRepository : IGamesRepository
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

    public async Task<Result<Game, Error>> GetById(Guid guid, CancellationToken cancellationToken)
    {
        var game = await _dbContext.Games.Include(g => g.Predictions)
            .FirstOrDefaultAsync(g => g.Id == guid, cancellationToken: cancellationToken);

        if (game is null)
            return Errors.General.NotFound(guid);

        return game;
    }

    public async Task<Guid> Save(Game game, CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);

        return game.Id;
    }
}