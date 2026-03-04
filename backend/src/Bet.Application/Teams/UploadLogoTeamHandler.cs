using Bet.Contracts.Commands.Teams;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Teams;

public class UploadLogoTeamHandler
{
    private readonly IMinioProvider _provider;
    private readonly ITeamsRepository _repository;
    private readonly ILogger<UploadLogoTeamHandler> _logger;

    public UploadLogoTeamHandler(
        IMinioProvider provider,
        ITeamsRepository repository,
        ILogger<UploadLogoTeamHandler> logger)
    {
        _provider = provider;
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UploadLogoTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        var teamFromDb = await _repository.GetById(command.TeamId, cancellationToken);
        if (teamFromDb.IsFailure)
            return teamFromDb.Error;

        var uploadToMinio =  await _provider.UploadLogoTeam(command, cancellationToken);
        if (uploadToMinio.IsFailure)
            return uploadToMinio.Error;

        var updateLogo =  teamFromDb.Value.UpdateLogo(command.TeamId.ToString());
        if (updateLogo.IsFailure)
            return updateLogo.Error;

        var result = await _repository.Save(teamFromDb.Value, cancellationToken);
        
        _logger.LogInformation("Update logo team with id {result}",
            result);

        return result;
    }
}