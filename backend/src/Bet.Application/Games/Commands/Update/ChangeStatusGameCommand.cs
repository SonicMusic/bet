using Bet.Application.Abstractions;

namespace Bet.Application.Games.Commands.Update;

public record ChangeStatusGameCommand(Guid GameId, string StatusGame) : ICommand;