using Bet.Application.Models;

namespace Bet.Application.IoC;

public interface IReadDbContext
{
    IQueryable<GameDto> Games { get; }
    public IQueryable<PredictionDto> Predictions { get; }
    public IQueryable<TeamDto> Teams { get; }
}