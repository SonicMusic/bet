using Bet.Application.IoC;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games.Delete;

public class DeleteGameHandler
{
    private readonly IGamesRepository _repository;
    private readonly ILogger<DeleteGameHandler> _logger;

    public DeleteGameHandler(
        IGamesRepository repository,
        ILogger<DeleteGameHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeleteGameCommand command,
        CancellationToken cancellationToken = default)
    {
        var gameFromDb = await _repository.GetById(command.Guid, cancellationToken);
        if (gameFromDb.IsFailure)
            return gameFromDb.Error;
        
        gameFromDb.Value.Delete();

        await _repository.Save(gameFromDb.Value, cancellationToken);
        
        _logger.LogInformation("Delete game with id {result}", gameFromDb.Value.Id);

        return gameFromDb.Value.Id;
    }
}