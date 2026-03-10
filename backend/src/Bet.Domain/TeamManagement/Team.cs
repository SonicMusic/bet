using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Domain.TeamManagement;

public class Team : Entity
{
    // ef core
    private Team()
    {
    }


    private Team(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }


    public new Guid Id { get; private set; }
    public string Name { get; private set; } = default!;

    public bool Logo { get; private set; } = false;

    public static Result<Team, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            name.Length > Constants.General.MAX_NAME_LENGTH ||
            name.Length < Constants.General.MIN_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Team));

        return new Team(name);
    }

    public UnitResult<Error> UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            name.Length > Constants.General.MAX_NAME_LENGTH ||
            name.Length < Constants.General.MIN_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Team));

        Name = name.Trim();

        return UnitResult.Success<Error>();
    }

    public void UploadLogo()
    {
        Logo = true;
    }
}