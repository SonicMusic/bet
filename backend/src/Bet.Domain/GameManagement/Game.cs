using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Domain.GameManagement;

public class Game : Entity
{
    private bool _isDeleted = false;

    // ef core
    private Game()
    {
        
    }

    private Game(Guid id, Guid homeTeamId, Guid awayTeamId)
    {
        Id = id;
        HomeTeamId = homeTeamId;
        AwayTeamId = awayTeamId;
    }
    
    public new Guid Id { get; private set; }
    
    public Guid HomeTeamId { get; private set; }  
    
    public Guid AwayTeamId { get; private set; }

    public static Result<Game, Error> Create(Guid homeTeamId, Guid awayTeamId)
    {
        if (string.IsNullOrWhiteSpace(homeTeamId.ToString()))
            return Errors.General.ValueIsRequired(); 
        if (string.IsNullOrWhiteSpace(awayTeamId.ToString()))
            return Errors.General.ValueIsRequired();
        if (homeTeamId == awayTeamId)
            return Errors.General.AlreadyExist();
        
        return new Game(Guid.NewGuid(), homeTeamId, awayTeamId);
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