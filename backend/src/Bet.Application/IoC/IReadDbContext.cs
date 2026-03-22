using Bet.Application.Models;
using Bet.Domain.GameManagement;
using Bet.Domain.TeamManagement;

namespace Bet.Infrastructure.Contexts;

public interface IReadDbContext
{
    IQueryable<GameDto> Games { get; }
    public IQueryable<PredictionDto> Predictions { get; }
    public IQueryable<TeamDto> Teams { get; }
}