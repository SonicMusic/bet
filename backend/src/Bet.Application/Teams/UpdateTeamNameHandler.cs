using Bet.Application.IoC;
using Bet.Contracts.Commands.Teams;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Teams;

public class UpdateTeamNameHandler
{
    private readonly ITeamsRepository _repository;
    private readonly ILogger<UpdateTeamNameHandler> _logger;

    public UpdateTeamNameHandler(
        ITeamsRepository repository,
        ILogger<UpdateTeamNameHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        var teamResult = await _repository.GetById(command.Id, cancellationToken);
        if (teamResult.IsFailure)
            return teamResult.Error;

        var team = teamResult.Value.UpdateName(command.Name);
        if (team.IsFailure)
            return team.Error;

        await _repository.Save(teamResult.Value, cancellationToken);

        _logger.LogInformation("Update team {name} with id {result}",
            command.Name, teamResult.Value.Id);

        return teamResult.Value.Id;
    }
}