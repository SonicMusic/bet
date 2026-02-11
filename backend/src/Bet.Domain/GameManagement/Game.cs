using CSharpFunctionalExtensions;

namespace Bet.Domain.GameManagement;

public class Game : Entity
{
    // ef core
    private Game()
    {
        
    }

    private Game(Team homeTeam, Team awayTeam)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
    }
    
    public Guid Id { get; private set; }
    public Team HomeTeam { get; private set; }
    public Team AwayTeam { get; private set; }
    public Guid HomeTeamId { get; private set; }     
    public Guid AwayTeamId { get; private set; }

    public static Result<Game> Create(Team homeTeam, Team awayTeam)
    {
        return new Game(homeTeam, awayTeam);
    }
}