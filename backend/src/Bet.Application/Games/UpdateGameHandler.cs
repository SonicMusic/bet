using Bet.Contracts.Commands.Games;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games;

public class UpdateGameHandler
{
    private readonly IGamesRepository _repository;
    private readonly ILogger<UpdateGameHandler> _logger;

    public UpdateGameHandler(
        IGamesRepository repository,
        ILogger<UpdateGameHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateGameCommand command,
        CancellationToken cancellationToken = default)
    {
        var gameFromDb = await _repository.GetById(command.GameId, cancellationToken);
        if (gameFromDb.IsFailure)
            return gameFromDb.Error;

        var gameResult = gameFromDb.Value.Update(command.HomeTeamId, command.AwayTeamId);
        if (gameResult.IsFailure)
            return gameResult.Error;

        await _repository.Save(gameFromDb.Value, cancellationToken);
        
        _logger.LogInformation("Update game with id {result}", gameFromDb.Value.Id);

        return gameFromDb.Value.Id;
    }
}