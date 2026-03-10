namespace Bet.Application.Predictions.Update;

public record UpdateStatusPredictionCommand(Guid GameId, Guid PredictionId, string Status);