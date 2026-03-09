namespace Bet.Application.Predictions.Update;

public record UpdateStatusPredictionCommand(Guid PredictionId, string Status);