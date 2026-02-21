using Bet.Contracts.Requests.Teams;
using Bet.Domain.TeamManagement;
using CSharpFunctionalExtensions;

namespace Bet.Application.Teams;

public class CreateTeamHandler
{
    private readonly ITeamsRepository _repository;

    public CreateTeamHandler(ITeamsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid, string>> Handle(
        CreateTeamRequest request, 
        CancellationToken cancellationToken = default)
    {
        var teamResult = Team.Create(request.Name);
        if (teamResult.IsFailure)
            return teamResult.Error;

        var result = await _repository.Add(teamResult.Value, cancellationToken);
        
        return result;
    }
}