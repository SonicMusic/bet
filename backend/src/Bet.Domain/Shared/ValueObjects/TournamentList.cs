namespace Bet.Domain.Shared.ValueObjects;

public record TournamentList
{
    
    //ef core
    private TournamentList()
    {
        
    }
    
    public TournamentList(List<Tournament> tournaments)
    {
        Tournaments = tournaments;
    }

    public IReadOnlyList<Tournament> Tournaments { get; }
};