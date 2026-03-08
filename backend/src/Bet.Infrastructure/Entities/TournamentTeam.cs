namespace Bet.Infrastructure.Entities;

/// <summary>
/// Join entity for many-to-many Tournament–Team (EF only, no domain logic).
/// </summary>
public class TournamentTeam
{
    public Guid TournamentId { get; set; }
    public Guid TeamId { get; set; }
}
