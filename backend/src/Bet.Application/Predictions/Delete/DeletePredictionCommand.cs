namespace Bet.Application.Predictions.Delete;

public record DeletePredictionCommand(Guid GameId, Guid PredictionId);