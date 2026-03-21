using Bet.Application.Abstractions;

namespace Bet.Application.Games.Delete;

public record DeleteGameCommand(Guid Guid) : ICommand;