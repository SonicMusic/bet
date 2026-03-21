using Bet.Application.Abstractions;
using Bet.Application.IoC;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Application.Teams;

public class GetTeamByNameHandler : ICommandHandler<Guid, GetTeamByNameCommand>
{
    private readonly ITeamsRepository _repository;

    public GetTeamByNameHandler(ITeamsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid, Error>> Handle(
        GetTeamByNameCommand command, 
        CancellationToken cancellationToken = default)
    {
        var team = await _repository.GetByName(command.Name, cancellationToken);
        if (team.IsFailure)
            return team.Error;

        return team.Value.Id;
    }
}