namespace Bet.Domain.TeamManagement;

public class Tournament
{
    //ef core
    private Tournament()
    {
        
    }

    public Tournament(string name, string nation)
    {
        Id = Guid.NewGuid();
        Name = name;
        Nation = nation;
    }
    
    public new Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Nation { get; private set; } = default!;
}