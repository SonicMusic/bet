namespace Bet.Application.Models;

public class GameDto
{
    public Guid GameId { get; init; }
    public Guid HomeTeamId { get; init; }
    public Guid AwayTeamId { get; init; }
    public int HomeTeamGoals { get; init; }
    public int AwayTeamGoals { get; init; }
    public PredictionDto[] Predictions { get; init; } = [];
    public string Status { get; init; } = String.Empty;
    public DateTimeOffset Start { get; init; }
}