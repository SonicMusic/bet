using Bet.Application.Abstractions;

namespace Bet.Application.Games.Commands.Update;

public record UpdateGameCommand(Guid GameId, string HomeTeam, string AwayTeam) : ICommand;