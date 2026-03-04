namespace Bet.Contracts.Commands.Teams;

public record UploadLogoTeamCommand(Guid TeamId, Stream Stream);