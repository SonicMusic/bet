using Bet.Application.Abstractions;

namespace Bet.Application.Teams;

public record GetTeamByNameCommand(string Name) : ICommand;