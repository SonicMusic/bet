using Bet.Domain.Shared;
using Bet.Domain.Shared.ValueObjects;
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
    public TournamentList? TournamentList { get; private set; }
    public bool Logo { get; private set; } = false;

    public static Result<Team, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || 
            name.Length > Constants.General.MAX_NAME_LENGTH ||
            name.Length < Constants.General.MIN_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Team));

        return new Team(Guid.NewGuid(), name);
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