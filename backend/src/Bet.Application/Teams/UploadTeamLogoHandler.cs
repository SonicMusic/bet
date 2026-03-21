using Bet.Application.Abstractions;
using Bet.Application.IoC;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Teams;

public class UploadTeamLogoHandler : ICommandHandler<Guid, UploadLogoTeamCommand>
{
    private readonly IMinioProvider _provider;
    private readonly ITeamsRepository _repository;
    private readonly ILogger<UploadTeamLogoHandler> _logger;

    public UploadTeamLogoHandler(
        IMinioProvider provider,
        ITeamsRepository repository,
        ILogger<UploadTeamLogoHandler> logger)
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

        teamFromDb.Value.UploadLogo();

        var result = await _repository.Save(teamFromDb.Value, cancellationToken);
        
        _logger.LogInformation("Update logo team with id {result}",
            result);

        return result;
    }
}