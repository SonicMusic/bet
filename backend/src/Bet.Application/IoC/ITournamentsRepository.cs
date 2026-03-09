using Bet.Domain.TeamManagement;

namespace Bet.Application.IoC;

public interface ITournamentsRepository
{
    Task<Guid> Add(Tournament tournament, CancellationToken cancellationToken);
}