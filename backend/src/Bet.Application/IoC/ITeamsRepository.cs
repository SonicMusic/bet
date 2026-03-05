using Bet.Domain.Shared;
using Bet.Domain.TeamManagement;
using CSharpFunctionalExtensions;

namespace Bet.Application.IoC;

public interface ITeamsRepository
{
    Task<Guid> Add(Team team, CancellationToken cancellationToken);
    Task<Result<Team, Error>> GetById(Guid guid, CancellationToken cancellationToken);
    Task<Result<Team, Error>> GetByName(string name, CancellationToken cancellationToken);
    Task<Guid> Save(Team team, CancellationToken cancellationToken);
}