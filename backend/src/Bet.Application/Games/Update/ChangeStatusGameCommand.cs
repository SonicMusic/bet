namespace Bet.Application.Games.Update;

public record ChangeStatusGameCommand(Guid GameId, string StatusGame);