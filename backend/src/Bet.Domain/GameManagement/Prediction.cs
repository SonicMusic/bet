using Bet.Domain.GameManagement.ValueObjects;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Domain.GameManagement;

public class Prediction : Entity, IIsDeletedField
{
    private bool _isDeleted = false;

    // ef core
    public Game Game { get; private set; }
    private Prediction()
    {
        ;
    }

    public Prediction(int homeTeamGoals, int awayTeamGoals)
    {
        Id = Guid.NewGuid();
        HomeTeamPredictedGoals = homeTeamGoals;
        AwayTeamPredictedGoals = awayTeamGoals;
    }

    public new Guid Id { get; private set; }
    public int HomeTeamPredictedGoals { get; private set; } = default;
    public int AwayTeamPredictedGoals { get; private set; } = default;
    public StatusPrediction Status { get; private set; } = StatusPrediction.Pending;

    public UnitResult<Error> SetStatus(StatusPrediction status)
    {
        Status = status;

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