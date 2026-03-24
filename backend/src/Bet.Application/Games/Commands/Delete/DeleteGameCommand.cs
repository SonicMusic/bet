using Bet.Application.Abstractions;

namespace Bet.Application.Games.Commands.Delete;

public record DeleteGameCommand(Guid Guid) : ICommand;