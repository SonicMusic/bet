using Bet.Application.IoC;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games.Update;

public class SetStartGameHandler
{
    private readonly IGamesRepository _repository;
    private readonly ILogger<SetStartGameHandler> _logger;

    public SetStartGameHandler(
        IGamesRepository repository, 
        ILogger<SetStartGameHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        SetStartGameCommand command,
        CancellationToken cancellationToken)
    {
        var gameFromDb = await _repository.GetById(command.GameId, cancellationToken);
        if (gameFromDb.IsFailure)
            return gameFromDb.Error;

        var result = gameFromDb.Value.SetStart(command.DateTimeOffset);
        if (result.IsFailure)
            return result.Error;
        
        await _repository.Save(gameFromDb.Value, cancellationToken);
        
        _logger.LogInformation("Set StartDateTime game with id {result}", gameFromDb.Value.Id);

        return gameFromDb.Value.Id;
    }
}