using Bet.Application.Abstractions;

namespace Bet.Application.Teams.Commands.Update;

public record UpdateTeamNameCommand(Guid Id, string Name) : ICommand;