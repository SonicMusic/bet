namespace Bet.Contracts.Requests.Predictions;

public record CreatePredictionRequest(int HomeTeamGoals, int AwayTeamGoals);