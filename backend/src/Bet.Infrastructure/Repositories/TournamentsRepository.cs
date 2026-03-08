using Bet.Application.IoC;
using Bet.Domain.Shared;
using Bet.Domain.TournamentManagement;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Bet.Infrastructure.Repositories;

public class TournamentsRepository : ITournamentsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TournamentsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Tournament tournament, CancellationToken cancellationToken)
    {
        await _dbContext.Tournaments.AddAsync(tournament, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return tournament.Id;
    }

    public async Task<Result<Tournament, Error>> GetById(Guid guid, CancellationToken cancellationToken)
    {
        var tournament = await _dbContext.Tournaments
            .FirstOrDefaultAsync(t => t.Id == guid, cancellationToken);

        if (tournament is null)
            return Errors.General.NotFound(guid);

        return tournament;
    }
    public async Task<Result<Tournament, Error>> GetByName(string name, CancellationToken cancellationToken)
    {
        var tournament = await _dbContext.Tournaments
            .FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
        
        if (tournament is null)
            return Errors.General.NotFound();
        
        return tournament;
    }

    public async Task<Guid> Save(Tournament tournament, CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);

        return tournament.Id;
    }
}