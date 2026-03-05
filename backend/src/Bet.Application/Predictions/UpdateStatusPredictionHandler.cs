using Bet.Application.IoC;
using Bet.Contracts.Commands.Predictions;
using Bet.Domain.PredictionManagement;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Predictions;

public class UpdateStatusPredictionHandler
{
    private readonly IPredictionsRepository _repository;
    private readonly ILogger<UpdateStatusPredictionHandler> _logger;

    public UpdateStatusPredictionHandler(
        IPredictionsRepository repository,
        ILogger<UpdateStatusPredictionHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateStatusPredictionCommand command, 
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<StatusPrediction>(command.Status, ignoreCase: true, out var status))
            return Errors.General.ValueIsInvalid($"Unknown status: {command.Status}");
    
        var prediction = await _repository.GetById(command.PredictionId, cancellationToken);
        if (prediction.IsFailure)
            return prediction.Error;

        var result = prediction.Value.SetStatus(status);
        if (result.IsFailure)
            return result.Error;

        await _repository.Save(prediction.Value, cancellationToken);
        
        _logger.LogInformation("Update the status {status} of the prediction id {prediction.Value.Id}",
            status, prediction.Value.Id);

        return prediction.Value.Id;
    }
}