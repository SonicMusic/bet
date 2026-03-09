using Bet.Application.IoC;
using Bet.Domain.Shared;
using Bet.Domain.TeamManagement;
using CSharpFunctionalExtensions;

namespace Bet.Application.Tournaments;

public class CreateTournamentHandler(ITournamentsRepository repository)
{
    public async Task<Result<Guid, Error>> Handle(
        CreateTournamentCommand command,
        CancellationToken cancellationToken = default)
    {
        var tournament = new Tournament(command.Name, command.Nation);

        await repository.Add(tournament, cancellationToken);

        return tournament.Id;
    }
}