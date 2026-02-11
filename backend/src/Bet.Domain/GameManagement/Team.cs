using CSharpFunctionalExtensions;

namespace Bet.Domain.GameManagement;

public class Team : Entity
{
    private readonly List<Game> _homeGames = [];
    private readonly List<Game> _awayGames = [];
    
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
    public int NumberGoalsScored { get; private set; }
    public IReadOnlyList<Game> HomeGames => _homeGames;
    public IReadOnlyList<Game> AwayGames => _awayGames;

    public static Result<Team> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Team>("Name can not be empty");
        
        return new Team(name);
    }
}