using Bet.Application.Abstractions;
using Bet.Application.IoC;
using Bet.Domain.GameManagement.ValueObjects;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Predictions.Update;

public class UpdateStatusPredictionHandler : ICommandHandler<Guid, UpdateStatusPredictionCommand>
{
    private readonly IGamesRepository _repository;
    private readonly ILogger<UpdateStatusPredictionHandler> _logger;

    public UpdateStatusPredictionHandler(
        IGamesRepository repository,
        ILogger<UpdateStatusPredictionHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateStatusPredictionCommand command, 
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<StatusPrediction>(command.Status, ignoreCase: true, out var statusPrediction))
            return Errors.General.ValueIsInvalid($"Unknown status: {command.Status}");
    
        var gameResult = await _repository.GetById(command.GameId, cancellationToken);
        if (gameResult.IsFailure)
            return gameResult.Error;

        var prediction = gameResult.Value.Predictions.FirstOrDefault(p => p.Id == command.PredictionId);
        if (prediction == null)
            return Errors.General.NotFound(command.PredictionId);

        var result = prediction.SetStatus(statusPrediction);
        if (result.IsFailure)
            return result.Error;

        await _repository.Save(gameResult.Value, cancellationToken);
        
        _logger.LogInformation("Update the status {status} of the prediction id {prediction}",
            statusPrediction, prediction.Id);

        return prediction.Id;
    }
}