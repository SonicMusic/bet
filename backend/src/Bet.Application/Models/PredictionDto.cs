namespace Bet.Application.Models;

public class PredictionDto
{
    public Guid Id { get; init; }
    public int HomeTeamGoals { get; init; }
    public int AwayTeamGoals { get; init; }
    public string Status { get; init; } = string.Empty;
}