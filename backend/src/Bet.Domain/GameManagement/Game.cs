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
    public int HomeTeamGoals { get; private set; } = default;
    public int AwayTeamGoals { get; private set; } = default;
    public StatusGame Status { get; private set; } = StatusGame.NotStarted;
    public DateTimeOffset Start { get; private set; } = DateTimeOffset.MaxValue;

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

    public UnitResult<Error> Update(Guid homeTeamId, Guid awayTeamId)
    {
        if (string.IsNullOrWhiteSpace(homeTeamId.ToString()))
            return Errors.General.ValueIsRequired(); 
        if (string.IsNullOrWhiteSpace(awayTeamId.ToString()))
            return Errors.General.ValueIsRequired();
        if (homeTeamId == awayTeamId)
            return Errors.General.AlreadyExist();

        HomeTeamId = homeTeamId;
        AwayTeamId = awayTeamId;
        
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> SetStatus(StatusGame statusGame)
    {
        Status = statusGame;
        
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> SetStart(DateTimeOffset dateTimeOffset)
    {
        Start = dateTimeOffset;
        
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