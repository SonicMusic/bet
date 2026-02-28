namespace Bet.Contracts.Commands.Predictions;

public record CreatePredictionCommand(Guid GameId, int HomeTeamGoals, int AwayTeamGoals);