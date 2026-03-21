using Bet.Application.Abstractions;

namespace Bet.Application.Predictions.Create;

public record CreatePredictionCommand(Guid GameId, int HomeTeamGoals, int AwayTeamGoals) : ICommand;