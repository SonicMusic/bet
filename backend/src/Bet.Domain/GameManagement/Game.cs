using Bet.Domain.GameManagement.ValueObjects;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Domain.GameManagement;

public class Game : Entity, IIsDeletedField
{
    private bool _isDeleted = false;
    private readonly List<Prediction> _predictions = [];

    // ef core
    private Game()
    {
        
    }

    private Game(Guid homeTeamId, Guid awayTeamId)
    {
        Id = Guid.NewGuid();
        HomeTeamId = homeTeamId;
        AwayTeamId = awayTeamId;
    }
    
    public new Guid Id { get; private set; }
    
    public Guid HomeTeamId { get; private set; }  
    public Guid AwayTeamId { get; private set; }
    public int HomeTeamGoals { get; private set; } = default;
    public int AwayTeamGoals { get; private set; } = default;
    public IReadOnlyList<Prediction> Predictions => _predictions;
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
        
        return new Game(homeTeamId, awayTeamId);
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
        if (dateTimeOffset < DateTimeOffset.Now)
            return Errors.General.ValueIsInvalid(dateTimeOffset.ToString());
        
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

    public UnitResult<Error> AddPrediction(Prediction prediction)
    {
        // add prediction if statusGame = notStarted
        if (Status != StatusGame.NotStarted)
            return Errors.General.ValueIsInvalid(Status.ToString());
        
        _predictions.Add(prediction);
        return UnitResult.Success<Error>();
    }
}