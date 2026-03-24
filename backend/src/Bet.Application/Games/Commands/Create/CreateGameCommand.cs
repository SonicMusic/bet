using Bet.Application.Abstractions;

namespace Bet.Application.Games.Commands.Create;

public record CreateGameCommand(string HomeTeam, string AwayTeam) : ICommand;