using Bet.Application.IoC;
using Bet.Domain.Shared;
using Bet.Domain.TeamManagement;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Teams.Create;

public class CreateTeamHandler
{
    private readonly ITeamsRepository _repository;
    private readonly ILogger<CreateTeamHandler> _logger;

    public CreateTeamHandler(
        ITeamsRepository repository,
        ILogger<CreateTeamHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        var teamFromDb = await _repository.GetByName(command.Name, cancellationToken);
        if (teamFromDb.IsFailure is false)
            return Errors.General.AlreadyExist();
        
        var teamResult = Team.Create(command.Name);
        if (teamResult.IsFailure)
            return Errors.General.ValueIsInvalid();

        var team = await _repository.Add(teamResult.Value, cancellationToken);

        _logger.LogInformation("Created team {name} with id {team}", command.Name, team);

        return team;
    }
}