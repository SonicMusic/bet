using Bet.Application.Abstractions;

namespace Bet.Application.Predictions.Commands.Delete;

public record DeletePredictionCommand(Guid GameId, Guid PredictionId) : ICommand;