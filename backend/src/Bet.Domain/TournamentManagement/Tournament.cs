using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Domain.TournamentManagement;

public class Tournament: Entity, IIsDeletedField
{
    private bool _isDeleted = false;

    
    //ef core
    private Tournament()
    {
        
    }

    public new Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Country { get; private set; }
    public bool Logo { get; private set; } = false;
    
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