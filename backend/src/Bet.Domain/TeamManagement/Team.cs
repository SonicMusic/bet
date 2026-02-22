using Bet.Domain.Shared;
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

    public static Result<Team, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.Default.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Team));

        return new Team(Guid.NewGuid(), name);
    }
}