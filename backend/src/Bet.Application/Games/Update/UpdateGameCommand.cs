using Bet.Application.Abstractions;

namespace Bet.Application.Games.Update;

public record UpdateGameCommand(Guid GameId, string HomeTeam, string AwayTeam) : ICommand;