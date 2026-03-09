namespace Bet.API.Requests.Predictions;

public record CreatePredictionRequest(int HomeTeamGoals, int AwayTeamGoals);