using Bet.Application.IoC;
using Bet.Domain.TeamManagement;

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
}