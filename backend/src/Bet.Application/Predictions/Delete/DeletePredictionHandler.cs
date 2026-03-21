using Bet.Application.Abstractions;
using Bet.Application.IoC;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Predictions.Delete;

public class DeletePredictionHandler : ICommandHandler<Guid, DeletePredictionCommand>
{
    private readonly IGamesRepository _repository;
    private readonly ILogger<DeletePredictionHandler> _logger;

    public DeletePredictionHandler(
        IGamesRepository repository, 
        ILogger<DeletePredictionHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeletePredictionCommand command, 
        CancellationToken cancellationToken)
    {
        var gameFromDb = await _repository.GetById(command.GameId, cancellationToken);
        if (gameFromDb.IsFailure)
            return gameFromDb.Error;

        var prediction = gameFromDb.Value.Predictions.FirstOrDefault(p => p.Id == command.PredictionId);
        if (prediction is null)
            return Errors.General.NotFound(command.PredictionId);
        
        prediction.Delete();

        var result = await _repository.Save(gameFromDb.Value, cancellationToken);
        
        _logger.LogInformation("Delete prediction with id {result}", result);

        return result;
    }
}