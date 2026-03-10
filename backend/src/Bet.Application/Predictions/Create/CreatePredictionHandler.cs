using Bet.Application.IoC;
using Bet.Domain.GameManagement;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Predictions.Create;

public class CreatePredictionHandler
{
    private readonly IGamesRepository _gamesRepository;
    private readonly ILogger<CreatePredictionHandler> _logger;

    public CreatePredictionHandler(
        IGamesRepository gamesRepository,
        ILogger<CreatePredictionHandler> logger)
    {
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

        var prediction = new Prediction(command.HomeTeamGoals, command.AwayTeamGoals);
        
        var result = gameResult.Value.AddPrediction(prediction);
        if (result.IsFailure)
            return result.Error;

        await _gamesRepository.Save(gameResult.Value, cancellationToken);
        
        _logger.LogInformation("Created prediction id {prediction}", prediction.Id);

        return prediction.Id;
    }
}