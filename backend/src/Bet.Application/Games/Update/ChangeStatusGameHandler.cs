using Bet.Application.IoC;
using Bet.Domain.GameManagement.ValueObjects;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games.Update;

public class ChangeStatusGameHandler
{
    private readonly IGamesRepository _repository;
    private readonly ILogger<ChangeStatusGameHandler> _logger;

    public ChangeStatusGameHandler(
        IGamesRepository repository, 
        ILogger<ChangeStatusGameHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        ChangeStatusGameCommand command, 
        CancellationToken cancellationToken)
    {
        var gameFromDb = await _repository.GetById(command.GameId, cancellationToken);
        if (gameFromDb.IsFailure)
            return gameFromDb.Error;
        
        if (!Enum.TryParse<StatusGame>(command.StatusGame, ignoreCase: true, out var statusGame))
            return Errors.General.ValueIsInvalid($"Unknown status: {command.StatusGame}");

        var result = gameFromDb.Value.ChangeStatus(statusGame);
        if (result.IsFailure)
            return result.Error;
        
        await _repository.Save(gameFromDb.Value, cancellationToken);
        
        _logger.LogInformation("Change the status {status} of the game id {game}",
            statusGame, gameFromDb.Value.Id);

        return gameFromDb.Value.Id;
    }
}