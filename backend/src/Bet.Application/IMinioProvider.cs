using Bet.Contracts.Commands.Teams;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Application;

public interface IMinioProvider
{
    Task<Result<string, Error>> UploadLogoTeam(
        UploadLogoTeamCommand command,
        CancellationToken cancellationToken = default);
}