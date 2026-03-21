using Bet.Application.Abstractions;

namespace Bet.Application.Games.Update;

public record SetStartGameCommand(Guid GameId, DateTimeOffset DateTimeOffset) : ICommand;