using Bet.Domain.Shared;
using Bet.Domain.TournamentManagement;
using CSharpFunctionalExtensions;

namespace Bet.Application.IoC;

public interface ITournamentsRepository
{
    Task<Guid> Add(Tournament tournament, CancellationToken cancellationToken);
    Task<Result<Tournament, Error>> GetById(Guid guid, CancellationToken cancellationToken);
    Task<Result<Tournament, Error>> GetByName(string name, CancellationToken cancellationToken);
    Task<Guid> Save(Tournament tournament, CancellationToken cancellationToken);
}