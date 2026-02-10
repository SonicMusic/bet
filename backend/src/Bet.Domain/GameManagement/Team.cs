using CSharpFunctionalExtensions;

namespace Bet.Domain.GameManagment;

public class Team : Entity
{
    private readonly List<Game> _games = [];
    
    // ef core
    private Team()
    {
        
    }

    private Team(string name)
    {
        Name = name;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string ShortName { get; private set; }
    public IReadOnlyList<Game> Games => _games;

    public static Result<Team> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Team>("Name can not be empty");
        
        return new Team(name);
    }
}