using Bet.Application.Abstractions;

namespace Bet.Application.Games.Create;

public record CreateGameCommand(string HomeTeam, string AwayTeam) : ICommand;