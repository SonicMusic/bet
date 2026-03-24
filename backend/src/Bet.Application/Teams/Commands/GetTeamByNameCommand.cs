using Bet.Application.Abstractions;

namespace Bet.Application.Teams.Commands;

public record GetTeamByNameCommand(string Name) : ICommand;