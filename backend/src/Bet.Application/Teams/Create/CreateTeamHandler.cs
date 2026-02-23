using Bet.Contracts.Requests.Teams;
using Bet.Domain.Shared;
using Bet.Domain.TeamManagement;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Teams.CreateTeam;

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
        CreateTeamRequest request, 
        CancellationToken cancellationToken = default)
    {
        
        
        var teamResult = Team.Create(request.Name);
        if (teamResult.IsFailure)
            return Errors.General.ValueIsInvalid();

        var result = await _repository.Add(teamResult.Value, cancellationToken);
        
        _logger.LogInformation("Created team {name} with id {result}", request.Name, result);
        
        return result;
    }
}