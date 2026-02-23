using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Domain.TeamManagement;

public class Team : Entity
{
    private bool _isDeleted = false;

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

    public UnitResult<Error> UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.Default.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Team));

        Name = name;

        return UnitResult.Success<Error>();
    }

    public void Delete()
    {
        if (_isDeleted == false)
            _isDeleted = true;
    }
    public void Restore()
    {
        if (!_isDeleted) return;

        _isDeleted = false;
    }
}