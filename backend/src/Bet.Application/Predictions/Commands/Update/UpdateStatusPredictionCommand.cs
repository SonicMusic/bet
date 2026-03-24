using Bet.Application.Abstractions;

namespace Bet.Application.Predictions.Commands.Update;

public record UpdateStatusPredictionCommand(Guid GameId, Guid PredictionId, string Status) : ICommand;