using Bet.Application.Abstractions;

namespace Bet.Application.Teams.Commands;

public record UploadLogoTeamCommand(Guid TeamId, Stream Stream) : ICommand;