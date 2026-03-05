using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Domain.TeamManagement;

public class Team : Entity, IIsDeletedField
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
    public string Name { get; private set; } = default!;
    public string? ShortName { get; private set; }
    public string? Country { get; private set; }
    public bool Logo { get; private set; } = false;

    public static Result<Team, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || 
            name.Length > Constants.Team.MAX_NAME_LENGTH ||
            name.Length < Constants.Team.MIN_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Team));

        return new Team(Guid.NewGuid(), name);
    }

    public UnitResult<Error> UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || 
            name.Length > Constants.Team.MAX_NAME_LENGTH ||
            name.Length < Constants.Team.MIN_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Team));
        
        Name = name.Trim();
        
        return UnitResult.Success<Error>();
    }
    public void UploadLogo()
    {
        Logo = true;
    }
    public UnitResult<Error> UpdateInfo(string? shortName, string? country)
    {
        if (shortName != null &&
            (string.IsNullOrWhiteSpace(shortName) || shortName.Length > Constants.Team.MAX_NAME_LENGTH))
            return Errors.General.ValueIsInvalid(nameof(Team));

        if (country != null &&
            (string.IsNullOrWhiteSpace(country) || country.Length > Constants.Team.MAX_NAME_LENGTH))
            return Errors.General.ValueIsInvalid(nameof(Team));

        if (shortName != null)
            ShortName = shortName.Trim();
        if (country != null)
            Country = country.Trim();

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