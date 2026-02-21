using Bet.Domain.TeamManagement;
using CSharpFunctionalExtensions;

namespace Bet.Application;

public interface ITeamsRepository
{
    Task<Guid> Add(Team team, CancellationToken cancellationToken);
    Task<Result<Team, string>> GetById(Guid guid);
}