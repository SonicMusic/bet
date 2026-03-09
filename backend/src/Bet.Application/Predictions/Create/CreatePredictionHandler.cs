using Bet.Application.IoC;
using Bet.Domain.PredictionManagement;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Predictions.Create;

public class CreatePredictionHandler
{
    private readonly IPredictionsRepository _predictionsRepository;
    private readonly IGamesRepository _gamesRepository;
    private readonly ILogger<CreatePredictionHandler> _logger;

    public CreatePredictionHandler(
        IPredictionsRepository predictionsRepository,
        IGamesRepository gamesRepository,
        ILogger<CreatePredictionHandler> logger)
    {
        _predictionsRepository = predictionsRepository;
        _gamesRepository = gamesRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreatePredictionCommand command, 
        CancellationToken cancellationToken = default)
    {
        var gameResult = await _gamesRepository.GetById(command.GameId, cancellationToken);
        if (gameResult.IsFailure)
            return gameResult.Error;

        var predictionResult = Prediction.Create(gameResult.Value.Id, command.HomeTeamGoals, command.AwayTeamGoals);
        if (predictionResult.IsFailure)
            return predictionResult.Error;

        var prediction = await _predictionsRepository.Add(predictionResult.Value, cancellationToken);
        
        _logger.LogInformation("Created prediction id {prediction}", prediction);

        return prediction;
    }
}