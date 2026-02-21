using CSharpFunctionalExtensions;

namespace Bet.Domain.TeamManagement;

public class Team : Entity
{
    // ef core
    private Team()
    {
    }

    private Team(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public new Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Result<Team> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Team>("Name can not be empty");

        return new Team(Guid.NewGuid(), name);
    }
}