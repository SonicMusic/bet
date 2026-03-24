using Bet.Application.Abstractions;

namespace Bet.Application.Games.Commands.Update;

public record SetStartGameCommand(Guid GameId, DateTimeOffset DateTimeOffset) : ICommand;