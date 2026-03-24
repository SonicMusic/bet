using Bet.Application.Abstractions;

namespace Bet.Application.Teams.Commands.Create;

public record CreateTeamCommand(string Name) : ICommand;