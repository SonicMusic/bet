namespace Bet.Contracts.Commands.Predictions;

public record UpdateStatusPredictionCommand(Guid PredictionId, string Status);