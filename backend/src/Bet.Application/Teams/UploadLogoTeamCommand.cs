using Bet.Application.Abstractions;

namespace Bet.Application.Teams;

public record UploadLogoTeamCommand(Guid TeamId, Stream Stream) : ICommand;