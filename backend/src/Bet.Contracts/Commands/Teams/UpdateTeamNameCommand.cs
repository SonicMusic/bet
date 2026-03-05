namespace Bet.Contracts.Commands.Teams;

public record UpdateTeamNameCommand(Guid Id, string Name);