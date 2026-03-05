using Bet.Domain.GameManagement;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Application.IoC;

public interface IGamesRepository
{
    Task<Guid> Add(Game game, CancellationToken cancellationToken);
    Task<Result<Game, Error>> GetById(Guid guid, CancellationToken cancellationToken);
    Task<Guid> Save(Game game, CancellationToken cancellationToken);
}