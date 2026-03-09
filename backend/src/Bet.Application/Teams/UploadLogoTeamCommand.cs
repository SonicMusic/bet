namespace Bet.Application.Teams;

public record UploadLogoTeamCommand(Guid TeamId, Stream Stream);