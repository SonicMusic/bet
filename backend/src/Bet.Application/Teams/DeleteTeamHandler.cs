using Bet.Contracts.Commands.Teams;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Teams;

public class DeleteTeamHandler
{
    private readonly ITeamsRepository _repository;
    private readonly ILogger<DeleteTeamHandler> _logger;

    public DeleteTeamHandler(
        ITeamsRepository repository,
        ILogger<DeleteTeamHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeleteTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        var teamResult = await _repository.GetById(command.Guid, cancellationToken);
        if (teamResult.IsFailure)
            return teamResult.Error;

        teamResult.Value.Delete();

        await _repository.Save(teamResult.Value, cancellationToken);

        _logger.LogInformation("Delete team {name} with id {result}",
            teamResult.Value.Name,
            teamResult.Value.Id);

        return teamResult.Value.Id;
    }
}