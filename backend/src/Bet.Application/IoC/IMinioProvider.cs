using Bet.Application.Teams;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Application.IoC;

public interface IMinioProvider
{
    Task<Result<string, Error>> UploadLogoTeam(
        UploadLogoTeamCommand command,
        CancellationToken cancellationToken = default);
}