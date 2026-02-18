using Bet.Domain.TeamManagement;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Bet.Infrastructure.Repositories;

public class TeamsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TeamsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Team team, CancellationToken cancellationToken)
    {
        await _dbContext.Teams.AddAsync(team, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return team.Id;
    }

    public async Task<Result<Team, string>> GetById(Guid guid)
    {
        var team = await _dbContext.Teams
            .FirstOrDefaultAsync(t => t.Id == guid);

        if (team is null)
            return "Team not found";

        return team;
    }
}